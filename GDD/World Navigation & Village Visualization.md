# 🗺️ Ironfrost Sagas — World Navigation & Village Visualization

> Design document covering the world map system: navigation UX, island/region structure, village distribution, world generation strategies, and the load-on-demand architecture for efficient front-back communication.
>
> **Relates to:** GDD §8.3 World · §8.4 World View · Data Model: `World`, `Village(x, y)`

---

## Table of Contents

1. [Navigation UX — System Design](#1-navigation-ux--system-design)
2. [World Structure — Islands, Regions & Village Layout](#2-world-structure--islands-regions--village-layout)
3. [Village Distribution — Free Slots & Barbarian Villages](#3-village-distribution--free-slots--barbarian-villages)
4. [World Generation — Strategies & Ideation](#4-world-generation--strategies--ideation)
5. [Load-on-Demand Architecture](#5-load-on-demand-architecture)
6. [Front–Back Communication Protocol](#6-frontback-communication-protocol)
7. [Competitor Analysis — How They Do It](#7-competitor-analysis--how-they-do-it)
8. [Open Questions & Next Steps](#8-open-questions--next-steps)

---

## 1. Navigation UX — System Design

### 1.1 Concept

The world map is the **primary strategic view** of Ironfrost Sagas. The player sees the entire game world from an overhead perspective — a vast Nordic sea with islands dotted across it, each island holding a cluster of villages.

Directly inspired by **Grepolis** (island-based cluster model) and **Tribal Wars** (continuous grid model), Ironfrost Sagas proposes a **hybrid approach**: a continuous coordinate grid (`x, y`) overlaid with a thematic island clustering layer for visual identity and strategic zoning.

### 1.2 Navigation Controls

| Action | Mouse / Touch | Description |
|--------|:------:|-------------|
| **Pan** | Click + drag / Two-finger drag | Moves the camera across the world map |
| **Zoom In** | Scroll up / Pinch out | Reveals more village detail |
| **Zoom Out** | Scroll down / Pinch in | Shows broader strategic picture |
| **Click village** | Left click / Tap | Opens village info panel |
| **Right-click** | Right click / Long press | Opens context menu (attack, send support, scout) |
| **Jump to own village** | Keyboard shortcut / HUD button | Centers view on player's village |
| **Search** | HUD search bar | Find a player, village, or island by name |

### 1.3 Zoom Level States

The map operates in **3 distinct zoom levels**, each with different information density:

```
ZOOM LEVEL 1 — Strategic Overview (zoomed out)
  • Shows: Island outlines, region color-coding by dominant Aett (clan)
  • Hides: Individual village icons, player names
  • Use case: Planning attacks, reading alliance territory control

ZOOM LEVEL 2 — Island View (mid zoom)
  • Shows: Village icons (color-coded by owner/barbarian/free), island name
  • Hides: Individual village stats, building detail
  • Use case: Choosing attack targets, reading local power balance

ZOOM LEVEL 3 — Village View (zoomed in)
  • Shows: Village name, owner name, point total, troop indicator (army present? yes/no)
  • Full tooltip on hover/tap: Owner, points, Aett, loyalty (if own troops are there)
  • Use case: Target selection, scouting intelligence reading
```

### 1.4 Visual Identity of the Map

The world map should feel **Nordic and alive**:

- **Background:** A dark, stormy Nordic sea — deep teal/grey water with subtle animated waves.
- **Islands:** Rendered as distinct land masses with visible terrain variation (snowy peaks, forests, rocky coasts). Each island has a unique visual character.
- **Villages:** Small icons on the island. Color-coded ring or flag indicates ownership:
  - 🟤 Barbarian / unclaimed
  - 🔵 Enemy player
  - 🟢 Friendly / Aett member
  - 🔴 Your own village
  - ⚪ Empty slot (available for new player)
- **Fog of War:** Areas far from any player village are darkened. Fog lifts around owned villages and recently scouted areas.
- **Activity Indicators:** Moving troop lines (animated dashed arrows) show incoming/outgoing attacks in real time.

---

## 2. World Structure — Islands, Regions & Village Layout

### 2.1 Coordinate System

The world uses a **flat 2D integer grid** where each cell `(x, y)` can hold one village:

```
World grid: width × height cells
  • Each cell = one potential village slot
  • Not all cells are occupied — density varies by region
  • Coordinates stored as short (−32768 to 32767) → max 65,535 × 65,535 world
```

For an initial world (MVP), a **500 × 500 grid** (250,000 cells) with ~30% occupancy = ~75,000 possible village slots is a manageable starting point.

### 2.2 Island Structure

Islands are **logical groupings** of village slots, not physical barriers. An island:

- Covers a **roughly circular or irregular cluster** of cells in the grid
- Contains between **10 and 30 village slots** (configurable per world)
- Has a **unique name** (drawn from Norse mythology — Yggdrasil, Bifröst, etc.)
- Has a **unique ID** referenced for display and gameplay mechanics
- Is separated from adjacent islands by **empty sea cells** (no villages in the gaps)

```
Island example — "Ísafjörð" (Island #42)
  Center: (240, 180)
  Radius: ~5 cells
  Village slots: 16 total
    • 8 occupied (player villages)
    • 4 barbarian villages
    • 4 empty (open for new players)
```

### 2.3 Region Structure

Islands are grouped into **Regions** — named zones of the world with distinct visual identity and potential gameplay significance (terrain bonuses, event zones, realm gates):

```
Region examples:
  • Jötunheimr Reaches  — northern quadrant, icy terrain, Frost Giant lore
  • Midgard Heartlands  — central zone, most populated, balanced resources
  • Freyja's Isles      — southeastern cluster, warmer aesthetic, Freyja bonus
  • The Bifröst Straits — narrow corridor between regions, high-traffic PvP zone
  • Yggdrasil's Root    — world center, high-value, contested by top players
```

| Entity | Scale | Contains |
|--------|:-----:|----------|
| World | 1 per server instance | All regions |
| Region | 4–9 per world | ~10–20 islands |
| Island | ~100–200 per world | 10–30 village slots |
| Village | ~75,000 total | 1 per grid cell |

### 2.4 Village Slot States

Each village slot in the world can be in one of four states:

| State | Description | Visual |
|-------|-------------|:------:|
| **Owned** | Belongs to an active player | Colored flag (owner's color) |
| **Barbarian** | Abandoned or auto-generated NPC village. Attackable and conquerable | Faded/dark icon 🟤 |
| **Empty** | Slot with no village — reserved for new player placement | Subtle marker ⚪ |
| **Blocked** | Permanently blocked (terrain, world border, special feature) | — (no icon) |

---

## 3. Village Distribution — Free Slots & Barbarian Villages

### 3.1 New Player Placement Algorithm

When a new player joins the world, the backend must find them an appropriate starting village slot. The goal is **fair placement** — not too isolated (boring), not dumped next to powerful players (unfair).

```
NewPlayerPlacement():

1. Find the player's assigned Region
   • New players → assigned to the lowest-population region
   • Prevents overcrowding; funnels new players to frontier zones

2. Within that region, find Islands with ≥ 1 Empty slot

3. Score candidate islands:
   score = (empty_slots × 2)
         − (nearby_powerful_players × 5)    ← penalty for danger
         + (nearby_barbarian_villages × 1)  ← bonus (easy early targets)
         − (distance_to_region_center × 0.1) ← slight center preference

4. Select top 3 candidates → present to player as choices
   ("Choose your landing point on Midgard")

5. Player confirms → slot state changes Empty → Owned
   → Village is seeded with starter buildings (Jarl's Hall Lv1, Barracks Lv1, etc.)
```

> **Design Note:** Giving the player **3 options** for placement is a mechanic borrowed from Grepolis. It creates a feel of agency without overwhelming complexity, and the choice itself teaches players to read the map.

### 3.2 Barbarian Village Seeding

Barbarian villages serve two gameplay roles:
1. **Early game PvE targets** — safe to attack, good for resource practice
2. **Late game fillers** — when a player leaves, their village becomes barbarian and remains a valid target

**Seeding rules at world creation:**
- ~40% of all village slots start as **Barbarian**
- ~20% start as **Empty** (reserved for incoming players)
- ~40% start as **Empty** (never pre-populated — will be used as world grows)

**Barbarian village scaling:**
```
Barbarian villages start weak and grow slowly over time:
  Week 1:  Jarl's Hall Lv 1–3, minimal troops
  Month 1: Jarl's Hall Lv 5–8, basic defending troop count
  Month 3+: Jarl's Hall Lv 10+, meaningful defense (NPC auto-build logic)

This ensures early players always have accessible targets
while late-joining players still find challenging barbarians to fight.
```

### 3.3 Island Ownership & Aett Dominance

An island can be **dominated** by an Aett when that clan owns the majority of villages on it. This is displayed visually:

- Dominant Aett's color washes over the island in Zoom Level 1
- Island name shows the dominant Aett tag: `[FROST] Ísafjörð`
- Future mechanic: **Island Bonus** — control 75%+ of an island to get a resource bonus

---

## 4. World Generation — Strategies & Ideation

### 4.1 Three Approaches Compared

| Approach | Description | Pros | Cons |
|----------|-------------|------|------|
| **A) Pre-built Fixed Map** | World is hand-crafted by game designers. All island positions, names, and slot counts are fixed before launch | Full artistic control, balanced layout, no generation bugs | Inflexible, can't scale, boring after multiple worlds |
| **B) Fully Procedural** | Algorithm generates the entire world fresh for each server instance | Infinitely replayable, unique every world | Complex to balance, risk of unplayable layouts, hard to guarantee fairness |
| **C) Modular Template (Recommended ✅)** | World is assembled from pre-designed **island templates** placed procedurally on the grid | Balance of control + uniqueness, scalable, designer-friendly | Requires more upfront template work |

### 4.2 Recommended: Modular Template Generation

The world is generated by a server-side **World Builder** at world creation time. The process:

```
WorldBuilder Algorithm:

INPUT: world_size, player_capacity, seed (for reproducibility)

STEP 1 — Define Region Grid
  • Divide world into N × N regions (e.g., 3×3 = 9 regions for 500×500 world)
  • Each region gets a biome type (Nordic coast, tundra, volcanic, etc.)
  • Center region = "Yggdrasil's Core" (highest value, always exists)

STEP 2 — Place Islands Within Each Region
  • Randomly select from a pool of pre-designed island templates
  • Island templates define: shape, village slot count, slot arrangement
  • Island templates are tagged by biome compatibility
  • Placement uses Poisson Disk Sampling to enforce minimum spacing between islands
    → No two island centers within 15 cells of each other
    → No island within 5 cells of world border

STEP 3 — Assign Names
  • Each island draws a unique name from a pool of Norse-themed names
  • Each region is assigned a lore name

STEP 4 — Seed Barbarian Villages
  • For each island, randomly populate ~50% of slots as barbarian
  • Reserve ~20% as empty starter slots

STEP 5 — Persist to Database
  • Write all Village records with (x, y, state=barbarian/empty, island_id)
  • Write all Island records
  • Write all Region records

OUTPUT: world_id + total village count + region map
```

### 4.3 Island Templates

Island templates are small JSON/config files defining the shape and slot layout of a reusable island. Example:

```json
{
  "template_id": "isle_medium_crescent",
  "slot_count": 14,
  "biomes": ["nordic_coast", "tundra"],
  "slots": [
    { "rel_x": 0,  "rel_y": 0  },
    { "rel_x": 1,  "rel_y": 0  },
    { "rel_x": 2,  "rel_y": 1  },
    { "rel_x": 2,  "rel_y": 2  },
    { "rel_x": 1,  "rel_y": 3  },
    { "rel_x": 0,  "rel_y": 3  },
    { "rel_x": -1, "rel_y": 2  }
    // ... 14 total
  ]
}
```

The World Builder rotates and mirrors templates randomly so the same template produces visually distinct islands.

**Minimum Template Pool for MVP:**

| Type | Slot Count | Count Needed |
|------|:----------:|:------------:|
| Small island | 6–10 | 5 templates |
| Medium island | 11–18 | 8 templates |
| Large island | 19–30 | 4 templates |
| Special (boss/realm gate) | 2–4 | 3 templates |
| **Total** | | **20 templates** |

### 4.4 Realm Gates (Optional — Post-MVP)

Certain island slots contain **Realm Gates** — fixed special structures granting access to the Nine Realms (Asgard, Jötunheimr, etc.). These are placed by the World Builder at specific strategic positions:

- 1 gate per region (9 gates total in a 3×3 region world)
- Positioned on specially-tagged island templates (`"type": "gate"`)
- No village can be conquered at a gate cell — it's a world feature, not a player slot

---

## 5. Load-on-Demand Architecture

### 5.1 The Problem

A world with 75,000+ villages **cannot be loaded into the client all at once**. Every village requires: position, name, owner info, point total, and visual state. Loading all of this on connection = ~5–15MB of data, plus rendering 75,000 map icons = unacceptable performance.

### 5.2 Chunk-Based Loading

The world is divided into **chunks** — square cells of the coordinate grid. The client only requests and renders chunks that are **within or near the current viewport**.

```
Chunk size: 20×20 cells (400 villages max per chunk)
World 500×500 = 625 total chunks

Client viewport at any zoom level = ~3×3 to 5×5 chunks visible
Active chunks loaded = visible chunks + 1 ring of buffer chunks
  → ~25–49 chunks in memory maximum at once
  → ~10,000–20,000 village records max in client memory
```

```
Chunk coordinate:
  chunk_x = floor(village_x / CHUNK_SIZE)
  chunk_y = floor(village_y / CHUNK_SIZE)

Chunk ID (cache key): "world_{id}_{chunk_x}_{chunk_y}"
```

### 5.3 Chunk Lifecycle

```
┌─────────────────────────────────────────────────────────────┐
│                    CLIENT CHUNK MANAGER                      │
│                                                              │
│  On camera move:                                             │
│    1. Calculate which chunks overlap the new viewport        │
│    2. Add buffer ring (+1 chunk on each side)                │
│    3. For each new chunk in view:                            │
│       a. Check local cache → if present & fresh → render    │
│       b. If absent or stale → request from backend          │
│    4. For each chunk leaving view (+ 2 rings away):          │
│       → Unload from memory, keep in disk cache (LRU)        │
└─────────────────────────────────────────────────────────────┘
```

### 5.4 Cache TTL Strategy

Not all chunks need to refresh at the same rate:

| Chunk Type | TTL | Reason |
|------------|:---:|--------|
| **Player's own chunk** | 10 seconds | Player needs near-real-time own village state |
| **Active combat chunk** | 30 seconds | Battles may be in progress |
| **Nearby chunks** (1–2 rings away) | 2 minutes | Changes happen but not constantly |
| **Far chunks** (3+ rings away) | 10 minutes | Low-frequency data, save bandwidth |
| **Zoom Level 1** (whole world overview) | 5 minutes | Coarse data, infrequent changes |

### 5.5 Pre-fetching Strategy

When the player **pans in a direction**, the chunk manager predicts where they're heading and pre-fetches chunks **before** they enter the viewport:

```
On sustained pan in direction D:
  → Pre-fetch 1 additional ring of chunks in direction D
  → Cancel pre-fetch if direction changes
```

This hides latency and creates a seamless scrolling experience even on slower connections.

---

## 6. Front–Back Communication Protocol

### 6.1 Overview

The world map uses **two communication channels**:

| Channel | Protocol | Use Case |
|---------|:--------:|----------|
| **HTTP REST** | Request/Response | Chunk loading, village detail, initial world load |
| **WebSocket** | Persistent bidirectional | Real-time troop movement arrows, conquest events, live point changes |

### 6.2 REST Endpoints — Chunk Loading

#### `GET /world/{world_id}/chunks`

Request chunks by a list of chunk coordinates:

```http
GET /world/abc123/chunks?chunks=10,12;10,13;11,12;11,13
Authorization: Bearer {token}
```

Response (one entry per chunk):

```json
{
  "world_id": "abc123",
  "chunks": [
    {
      "chunk_x": 10,
      "chunk_y": 12,
      "generated_at": "2026-04-09T13:00:00Z",
      "villages": [
        {
          "id": "vil_001",
          "x": 204, "y": 242,
          "name": "Frostheim",
          "owner_id": "usr_abc",
          "owner_name": "Thorvald",
          "aett_tag": "FROST",
          "points": 1842,
          "state": "owned"
        },
        {
          "id": "vil_002",
          "x": 206, "y": 243,
          "name": "—",
          "owner_id": null,
          "owner_name": null,
          "aett_tag": null,
          "points": 312,
          "state": "barbarian"
        }
      ]
    }
  ]
}
```

> **Payload size estimate:**  
> 1 village ≈ 150 bytes JSON  
> 1 chunk (400 villages max, ~200 avg occupied) ≈ 30KB  
> 9 chunks at once ≈ 270KB — acceptable for a single request

#### `GET /world/{world_id}/village/{village_id}`

Full village detail — triggered when player clicks a village icon:

```json
{
  "id": "vil_001",
  "name": "Frostheim",
  "owner_id": "usr_abc",
  "owner_name": "Thorvald",
  "aett_id": "aet_frost",
  "aett_name": "The Frostborn",
  "points": 1842,
  "loyalty": 100,
  "island_name": "Ísafjörð",
  "region_name": "Jötunheimr Reaches",
  "buildings_summary": {
    "jarl_hall_level": 12,
    "watch_tower_level": 8
  },
  "incoming_attacks": 2,
  "has_troops": true
}
```

> **Note:** `buildings_summary` only shows a brief snapshot — never the full building list of another player's village. Full details are only visible for own villages.

### 6.3 WebSocket — Real-Time Events

The WebSocket connection is established once at login and maintained throughout the session. The server pushes events to the client without polling:

```
Client → Server: SUBSCRIBE_CHUNKS [chunk_ids...]
  // Client tells server which chunks it currently has in viewport

Server → Client: VILLAGE_UPDATE
{
  "event": "village_update",
  "village_id": "vil_001",
  "changes": {
    "points": 1950,
    "owner_id": "usr_xyz",    ← conquest happened!
    "owner_name": "Bjornulf"
  }
}

Server → Client: MARCH_UPDATE
{
  "event": "march_update",
  "command_id": "cmd_abc",
  "origin": { "x": 204, "y": 242 },
  "target": { "x": 250, "y": 260 },
  "arrival_at": "2026-04-09T15:30:00Z",
  "type": "attack"
}

Server → Client: CHUNK_INVALIDATE
{
  "event": "chunk_invalidate",
  "chunk_x": 10,
  "chunk_y": 12
}
// Tells client to re-fetch this chunk on next viewport cycle
```

### 6.4 Client-Side State Management

```
WorldMapState {
  viewport:       { center_x, center_y, zoom_level }
  loaded_chunks:  Map<chunk_id, { data, loaded_at, ttl }>
  active_marches: Map<command_id, MarchData>
  subscriptions:  Set<chunk_id>   ← currently subscribed via WebSocket
}

On viewport change:
  → Compute required_chunks
  → Diff vs loaded_chunks
  → Request missing chunks via REST
  → Update WebSocket subscriptions: SUBSCRIBE_CHUNKS [newly visible]
  → Unsubscribe: UNSUBSCRIBE_CHUNKS [out of view]
  → Purge stale chunks from memory (LRU eviction)
```

### 6.5 Backend — Chunk Query Optimization

The backend serves chunk requests from:

1. **PostgreSQL query** (primary source):
```sql
SELECT id, x, y, name, owner_id, u.username as owner_name,
       u.aett_id, a.aett_name, v.points, v.state
FROM Village v
LEFT JOIN "User" u ON v.owner_id = u.id
LEFT JOIN Aett a ON u.aett_id = a.id
WHERE v.world_id = $1
  AND v.x BETWEEN $2 AND $3   -- chunk x range
  AND v.y BETWEEN $4 AND $5   -- chunk y range
```

2. **Redis Cache** (fast path — recommended):
   - Cache chunk responses in Redis with TTL matching the table above
   - Key: `chunk:{world_id}:{chunk_x}:{chunk_y}`
   - On village `UPDATE` → invalidate corresponding chunk key in Redis
   - Cache hit rate target: **>90%** for non-player-owned chunks

```
Request flow:
  Client → REST API → Redis cache?
    ├── HIT  → return cached JSON (< 5ms)
    └── MISS → query PostgreSQL → cache in Redis → return JSON (~20–50ms)
```

### 6.6 Spatial Index (PostgreSQL)

For efficient bounding-box queries on village positions, add a **spatial index**:

```sql
-- Composite index on (world_id, x, y) — covers the chunk query perfectly
CREATE INDEX idx_village_world_xy
  ON Village (world_id, x, y);

-- Alternative: if using PostGIS extension, use a geometry column
-- CREATE INDEX idx_village_geom ON Village USING GIST(geom);
```

---

## 7. Competitor Analysis — How They Do It

### 7.1 Tribal Wars

- **Grid:** Continuous flat grid, typically 500×500 or 1000×1000 depending on the world
- **Village distribution:** All village slots exist at world start, pre-assigned at fixed positions. New players are placed randomly in sparse areas.
- **No islands:** Villages are distributed across the entire map with no thematic clustering. Pure coordinate grid.
- **Loading:** Browser-based; loads visible quadrant on pan. Uses tile-based image rendering for the terrain layer + overlay for village icons.
- **World generation:** Manual/fixed maps per world. Each "world" is a distinct server with identical starting layout.

### 7.2 Grepolis

- **Grid:** Island-based. The world is a sea with fixed islands, each containing exactly 20 village slots.
- **Village distribution:** Islands have fixed slot counts. Players choose an island on registration. New players fill open slots on less-populated islands.
- **Islands:** Pre-designed, manually placed. The island layout is the same in all worlds of the same type (Speed world, Standard world).
- **Loading:** Tile-based web map (similar to Google Maps tiling). Loads tiles progressively as player pans.
- **Bonus islands:** Special "ghost towns" with farmable barbarian villages on separate island types.

### 7.3 Forge of Empires / Rise of Kingdoms (mobile)

- **Grid:** Hex grid (Forge) or isometric grid (RoK). Coordinate-based.
- **loading:** Heavy use of LOD (Level of Detail) — fewer details at low zoom, full detail at high zoom. Aggressive chunk streaming.
- **World generation:** Procedural with biome zones. Rise of Kingdoms uses a quadrant-based system where the map is divided into 4 kingdoms, each generated separately.

### 7.4 Summary Table

| Game | Grid Type | Island Model | World Gen | Chunk Loading |
|------|:---------:|:------------:|:---------:|:-------------:|
| Tribal Wars | Flat XY | None | Fixed per world | Tile-based |
| Grepolis | Flat XY | Fixed 20-slot islands | Pre-designed | Tile streaming |
| Forge of Empires | Hex grid | Zones | Procedural | LOD + chunks |
| Rise of Kingdoms | Isometric | Quadrant regions | Procedural | Chunk streaming |
| **Ironfrost Sagas** | **Flat XY** | **Modular templates** | **Modular procedural** | **Chunk + Redis cache** |

### 7.5 Key Insights from Competitors

1. **Grepolis island model works** — clustering villages on islands gives meaningful local rivalry and makes the map readable. Ironfrost should adopt this.
2. **Fixed slot count per island** (Grepolis: always 20) simplifies backend logic and UX. Worth adopting.
3. **Tile streaming** (Grepolis, TW) is proven for browser-based games. For a C++ native client, chunk-based JSON streaming is more appropriate.
4. **Redis/cache layer** is non-negotiable for MMO map queries — every competitor uses some form of caching for map data.
5. **New players should not spawn next to powerful players** — all competitors handle this through placement algorithms (lower-population area preference).

---

## 8. Open Questions & Next Steps

### Open Questions

| # | Question | Priority |
|:-:|----------|:--------:|
| 1 | **Fixed island slot count or variable?** Grepolis uses always-20. Variable is more flexible but harder to balance. | High |
| 2 | **World size for MVP?** 500×500 with 200 islands? Or smaller (200×200) for a faster prototype? | High |
| 3 | **Fog of war implementation?** Client-side masking or server-side filtering of chunk data? | Medium |
| 4 | **WebSocket or polling for real-time?** WebSocket is ideal but harder to scale initially. Long-polling as fallback? | Medium |
| 5 | **How many island templates needed before launch?** 20 seems sufficient for MVP variety. | Medium |
| 6 | **Realm Gates — in MVP or deferred?** Adds significant complexity; probably Post-MVP. | Low |
| 7 | **Mobile-first or desktop-first for navigation?** Touch controls require different UX treatment. | High |

### Next Steps

- [ ] Define exact world size for MVP (grid dimensions, island count, player capacity)
- [ ] Design the 20 island templates (coordinate offsets + biome tags)
- [ ] Define the REST API contract in a dedicated API spec document
- [ ] Prototype the chunk loading system in the C++ client (viewport → chunk → request cycle)
- [ ] Prototype the backend chunk query + Redis cache layer in Go
- [ ] Update GDD §8.3 and §8.4 with finalized system design (link this document)
- [ ] Update the Data Model to add `Island` and `Region` entities
