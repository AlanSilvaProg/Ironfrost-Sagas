<p align="center">
  <h1 align="center">⚔️ Ironfrost Sagas</h1>
  <p align="center"><strong>Nordic Real-Time MMORTS — Massive Multiplayer Online Real-Time Strategy Game</strong></p>
  <p align="center"><em>Game Design Document — v0.1 (Draft)</em></p>
</p>

---

## 📋 Table of Contents

1. [Mission](#1-mission)
2. [Challenges](#2-challenges)
3. [Technology Study](#3-technology-study)
4. [Concept](#4-concept)
5. [Visual](#5-visual)
6. [Market](#6-market)
7. [MVP — Minimum Viable Product](#7-mvp--minimum-viable-product)
8. [Game Design](#8-game-design)
   - 8.1 [Lore](#81-lore)
   - 8.2 [Tutorial](#82-tutorial)
   - 8.3 [World](#83-world)
   - 8.4 [World View](#84-world-view)
   - 8.5 [City View](#85-city-view)
   - 8.6 [Materials & Resources](#86-materials--resources)
   - 8.7 [Buildings](#87-buildings)
   - 8.8 [Citizens](#88-citizens)
   - 8.9 [Troops](#89-troops)
   - 8.10 [Heroes](#810-heroes)
   - 8.11 [Special Troops](#811-special-troops)
   - 8.12 [Ships](#812-ships)
   - 8.13 [Buff & Nerf Elements](#813-buff--nerf-elements)
   - 8.14 [Battles](#814-battles)
   - 8.15 [Points & Ranking](#815-points--ranking)
   - 8.16 [Conquest](#816-conquest)
   - 8.17 [Alliance](#817-alliance)
   - 8.18 [Messages](#818-messages)
   - 8.19 [Events & Seasons](#819-events--seasons)
   - 8.20 [NPCs](#820-npcs)
   - 8.21 [Exploration](#821-exploration)
9. [Architecture](#9-architecture)
10. [Roadmap](#10-roadmap)
11. [Appendix — Brainstorm & Research Notes](#11-appendix--brainstorm--research-notes)

---

## 1. Mission

Create a compelling RTS experience. The primary objective is to learn technologies such as **LiveOps** systems and other backend infrastructure. While commercial success is not the goal, the project serves as a deep learning exercise in game server architecture and real-time multiplayer systems.

---

## 2. Challenges

- Learn and apply a **functional backend** to an MMO game.
- Solve **on-demand graphics loading** for web/client environments.
- Implement a **dynamic system based on real clock time** (building queues, troop movements, etc.).
- Build **real-time connections** (WebSocket / gRPC) for player interactions.
- Design a scalable **infrastructure and architecture** for concurrent players.
- Build the client **from scratch using C++** and the backend using **Golang**.

---

## 3. Technology Study

| Layer              | Technology     | Notes                                        |
|--------------------|----------------|----------------------------------------------|
| **Client**         | C++            | Custom engine / rendering for web or native   |
| **Backend**        | Golang         | High-performance concurrent server            |
| **Database**       | PostgreSQL     | Relational DB for game state persistence      |
| **CDN**            | TBD            | Asset delivery                                |
| **Communication**  | TBD            | WebSocket / gRPC for real-time interactions   |

> **TODO:** Complete the technology study for C++ web development and Golang backend patterns.

---

## 4. Concept

| Attribute        | Value                    |
|------------------|--------------------------|
| **Main Reference** | Grépolis               |
| **Style**          | Browser-based MMORTS (similar to Grépolis / Tribal Wars) |
| **Theme**          | Nordic Mythology         |
| **Genre**          | MMO-RTS                  |

---

## 5. Visual

Similar to Grepolis.

> **TODO:** This section needs to be developed when art elements or concepts are finalized.

---

## 6. Market

> **TODO:** Complete market study — competitive landscape, target audience, monetization strategy.

---

## 7. MVP — Minimum Viable Product

The MVP must include the following core systems:

- [ ] City Build System
- [ ] Troops Build System
- [ ] Battle System
- [ ] World Navigation
- [ ] Message System

---

## 8. Game Design

### 8.1 Lore

A Nordic world in conflict — some call it **Ragnarök**.

With resources dwindling across the realms, all Norse kingdoms are drawn into fierce battles for survival and dominance. Creatures of legend emerge from the shadows, and the gods themselves take sides, blessing the cities they deem worthy.

The age of peace has ended. The age of iron and frost has begun.

---

### 8.2 Tutorial

The tutorial introduces the player to each core mechanic progressively:

| Step | Action                                           |
|------|--------------------------------------------------|
| 1    | Upgrade the first building (causing a resource to deplete) |
| 2    | Collect materials                                |
| 3    | Upgrade material source buildings                |
| 4    | Upgrade training centers                         |
| 5    | Train a new troop                                |
| 6    | Hunt animals                                     |
| 7    | Upgrade population buildings                     |
| 8    | Allocate population % to manual work (resource collecting & diplomacy) |
| 9    | Tutorial screens explaining **Motivation**, **Respect**, and **Population** |
| 10   | Attack a village to conquer it                   |

---

### 8.3 World

The player controls humans in **Midgard**. The world is a vast Nordic sea of islands, each holding clusters of villages across a flat 2D coordinate grid.

The world is divided into **Regions** (geographic zones with distinct lore identity) → **Islands** (village clusters of 10–30 slots each) → **Villages** (individual player or barbarian settlements).

Certain points on the map grant access to **other realms** (e.g., Asgard) via **Realm Gates**, where players can:
- Send troops to **explore** and gain experience.
- Complete **challenges** and **missions**.
- Search for rare **materials**.

> 📄 **Full system design, world generation, and load-on-demand architecture:** [World Navigation & Village Visualization](./World%20Navigation%20%26%20Village%20Visualization.md)

> **Design Note:** The Nine Worlds and Yggdrasil are integrated into the map as regions and realm gates — the map represents the Norse cosmos, with Midgard at the center and portals to outer realms at strategic positions.

---

### 8.4 World View

Overworld map with **3 zoom levels** — strategic overview (region control), island view (village icons), and village detail (names, owners, points).

Navigation uses pan + scroll/pinch. Village icons are color-coded by ownership state (own / friendly / enemy / barbarian / empty). Moving troop lines animate in real time via WebSocket. The map loads villages in **20×20 cell chunks on demand** as the player navigates — no full-world download.

> 📄 **Navigation UX, chunk loading, and front-back communication details:** [World Navigation & Village Visualization](./World%20Navigation%20%26%20Village%20Visualization.md)

---

### 8.5 City View

Isometric/top-down city view, keeping the classic MMO-RTS game style (Grepolis / Tribal Wars).

---

### 8.6 Materials & Resources

#### Materials (Gathered / Extracted)

| Material    | Usage                                  | Source             |
|-------------|----------------------------------------|--------------------|
| 🪵 **Wood**    | Training troops & constructing buildings | Woods              |
| 🪨 **Rock**    | Training troops & constructing buildings | Quarry             |
| ⛏️ **Metal**   | Training troops & constructing buildings | Cave               |
| ✨ **Eldrunar** | Training special troops & enchantments   | Cave (magical vein)|

#### Resources (Consumable)

| Resource         | Usage                                           | Source          |
|------------------|--------------------------------------------------|-----------------|
| 🥩 **Meat**       | Sustains citizens; required for population growth | Sacred Harvest Field / Hunting |
| 🦌 **Animal Skin** | Required for troop training                      | Hunting         |

---

### 8.7 Buildings

#### Building Upgrade Cost Formula

Each building has a **base cost per resource** and a **cost growth rate** (percentage increase per level):

```
CostAtLevel(L) = floor( BaseCost × CostGrowth ^ (L - 1) )
```

> **Design Rationale:** Buildings with fewer max levels have higher cost growth rates (more aggressive scaling), while buildings with many levels use gentler growth to keep early levels accessible. This naturally creates a cost curve where initial upgrades are cheap and fast, but the final levels represent significant investment.

---

#### Building Overview

| #  | Building                 | Function                        | Max Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | ✨ Eldrunar | Cost Growth |
|----|--------------------------|:-------------------------------:|:---------:|:------:|:------:|:-------:|:----------:|:-----------:|
| 1  | **Jarl's Hall**          | City Center                     | 20        | 90     | 120    | 60      | —          | 28%         |
| 2  | **Freyja's Temple**      | Divine Worship · Mythological Troop Training | 20        | 60     | 100    | 40      | 15         | 25%         |
| 3  | **Berserker's Barrack**  | Troop Training (Basic, Advanced, Elite) | 35        | 80     | 70     | 90      | —          | 18%         |
| 4  | **Drakkar Shipyard**     | Ship Construction               | 20        | 140    | 60     | 80      | —          | 25%         |
| 5  | **Explorer's House**     | Exploration & Espionage         | 10        | 50     | 40     | 30      | —          | 30%         |
| 6  | **Eldrunar's Cave**      | Metal & Magical Resource Source | 40        | 40     | 90     | 50      | —          | 15%         |
| 7  | **Runic Sanctuary**      | Research & Magic Upgrades       | 30        | 70     | 80     | 60      | 20         | 20%         |
| 8  | **Storage & Market**     | Resource Management             | 20        | 100    | 80     | 30      | —          | 22%         |
| 9  | **Sacred Harvest Field** | Food Production                 | 40        | 60     | 30     | 15      | —          | 14%         |
| 10 | **Watch Tower**          | Defense & Alert                 | 20        | 50     | 110    | 70      | —          | 24%         |
| 11 | **Víðarr's Woodland**    | Wood Production                 | 40        | 30     | 50     | 20      | —          | 14%         |
| 12 | **Dvergr's Quarry**      | Rock Production                 | 40        | 55     | 35     | 25      | —          | 15%         |

> ℹ️ The columns **Wood**, **Rock**, **Metal**, and **Eldrunar** show the **base cost (Level 1)**. Each subsequent level multiplies by `(1 + Cost Growth)`.

---

#### Cost Progression Tables

<details>
<summary><strong>1. Jarl's Hall</strong> — Growth: 28% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 90     | 120    | 60      | 270        |
| 2     | 115    | 153    | 76      | 344        |
| 3     | 147    | 196    | 98      | 441        |
| 4     | 188    | 251    | 125     | 564        |
| 5     | 241    | 322    | 161     | 724        |
| 10    | 830    | 1,106  | 553     | 2,489      |
| 15    | 2,852  | 3,802  | 1,901   | 8,555      |
| 20    | 9,800  | 13,066 | 6,533   | 29,399     |
</details>

<details>
<summary><strong>2. Freyja's Temple</strong> — Growth: 25% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | ✨ Eldrunar | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|:----------:|
| 1     | 60     | 100    | 40      | 15         | 215        |
| 2     | 75     | 125    | 50      | 18         | 268        |
| 3     | 93     | 156    | 62      | 23         | 334        |
| 4     | 117    | 195    | 78      | 29         | 419        |
| 5     | 146    | 244    | 97      | 36         | 523        |
| 10    | 447    | 745    | 298     | 111        | 1,601      |
| 15    | 1,364  | 2,273  | 909     | 341        | 4,887      |
| 20    | 4,163  | 6,938  | 2,775   | 1,040      | 14,916     |
</details>

<details>
<summary><strong>3. Berserker's Barrack</strong> — Growth: 18% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 80     | 70     | 90      | 240        |
| 2     | 94     | 82     | 106     | 282        |
| 3     | 111    | 97     | 125     | 333        |
| 4     | 131    | 115    | 147     | 393        |
| 5     | 155    | 135    | 174     | 464        |
| 10    | 354    | 310    | 399     | 1,063      |
| 15    | 811    | 710    | 913     | 2,434      |
| 20    | 1,857  | 1,625  | 2,089   | 5,571      |
| 25    | 4,248  | 3,717  | 4,779   | 12,744     |
| 30    | 9,720  | 8,505  | 10,935  | 29,160     |
| 35    | 22,237 | 19,457 | 25,016  | 66,710     |
</details>

<details>
<summary><strong>4. Drakkar Shipyard</strong> — Growth: 25% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 140    | 60     | 80      | 280        |
| 2     | 175    | 75     | 100     | 350        |
| 3     | 218    | 93     | 125     | 436        |
| 4     | 273    | 117    | 156     | 546        |
| 5     | 341    | 146    | 195     | 682        |
| 10    | 1,043  | 447    | 596     | 2,086      |
| 15    | 3,183  | 1,364  | 1,818   | 6,365      |
| 20    | 9,714  | 4,163  | 5,551   | 19,428     |
</details>

<details>
<summary><strong>5. Explorer's House</strong> — Growth: 30% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 50     | 40     | 30      | 120        |
| 2     | 65     | 52     | 39      | 156        |
| 3     | 84     | 67     | 50      | 201        |
| 4     | 109    | 87     | 65      | 261        |
| 5     | 142    | 114    | 85      | 341        |
| 10    | 530    | 424    | 318     | 1,272      |
</details>

<details>
<summary><strong>6. Eldrunar's Cave</strong> — Growth: 15% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 40     | 90     | 50      | 180        |
| 2     | 46     | 103    | 57      | 206        |
| 3     | 52     | 119    | 66      | 237        |
| 5     | 69     | 157    | 87      | 313        |
| 10    | 140    | 316    | 175     | 631        |
| 15    | 283    | 636    | 353     | 1,272      |
| 20    | 569    | 1,280  | 711     | 2,560      |
| 25    | 1,145  | 2,576  | 1,431   | 5,152      |
| 30    | 2,303  | 5,181  | 2,878   | 10,362     |
| 35    | 4,632  | 10,422 | 5,790   | 20,844     |
| 40    | 9,316  | 20,963 | 11,646  | 41,925     |
</details>

<details>
<summary><strong>7. Runic Sanctuary</strong> — Growth: 20% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | ✨ Eldrunar | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|:----------:|
| 1     | 70     | 80     | 60      | 20         | 230        |
| 2     | 84     | 96     | 72      | 24         | 276        |
| 3     | 100    | 115    | 86      | 28         | 329        |
| 5     | 145    | 165    | 124     | 41         | 475        |
| 10    | 361    | 412    | 309     | 103        | 1,185      |
| 15    | 898    | 1,027  | 770     | 256        | 2,951      |
| 20    | 2,236  | 2,555  | 1,916   | 638        | 7,345      |
| 25    | 5,564  | 6,359  | 4,769   | 1,589      | 18,281     |
| 30    | 13,846 | 15,825 | 11,868  | 3,956      | 45,495     |
</details>

<details>
<summary><strong>8. Storage & Market</strong> — Growth: 22% per level</summary>

**Upgrade Costs:**

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 100    | 80     | 30      | 210        |
| 2     | 122    | 97     | 36      | 255        |
| 3     | 148    | 119    | 44      | 311        |
| 5     | 221    | 177    | 66      | 464        |
| 10    | 598    | 478    | 179     | 1,255      |
| 15    | 1,618  | 1,294  | 485     | 3,397      |
| 20    | 4,373  | 3,498  | 1,312   | 9,183      |

**Storage Capacity:**

Each level increases the maximum amount of **each resource** your village can hold. Without this building, the village has a minimal default storage.

```
MaxPerResource(L) = floor( 1000 × 1.18 ^ (L - 1) )
```

| Level | Max per Resource | Total Capacity (6 resources) |
|:-----:|:----------------:|:----------------------------:|
| 0 *(no building)* | 500       | 3,000                        |
| 1     | 1,000            | 6,000                        |
| 2     | 1,180            | 7,080                        |
| 3     | 1,392            | 8,352                        |
| 4     | 1,643            | 9,858                        |
| 5     | 1,938            | 11,628                       |
| 6     | 2,287            | 13,722                       |
| 7     | 2,699            | 16,194                       |
| 8     | 3,185            | 19,110                       |
| 9     | 3,758            | 22,548                       |
| 10    | 4,435            | 26,610                       |
| 11    | 5,233            | 31,398                       |
| 12    | 6,175            | 37,050                       |
| 13    | 7,287            | 43,722                       |
| 14    | 8,599            | 51,594                       |
| 15    | 10,147           | 60,882                       |
| 16    | 11,973           | 71,838                       |
| 17    | 14,129           | 84,774                       |
| 18    | 16,672           | 100,032                      |
| 19    | 19,673           | 118,038                      |
| 20    | 23,214           | 139,284                      |

> 📦 **Max storage at level 20: 23,214 per resource** (139,284 total across Wood, Rock, Metal, Eldrunar, Meat, Animal Skin)

> ⚠️ **Overflow rule**: Any resources collected beyond the storage cap are **lost**. Players must balance upgrading storage with expanding production.
</details>

<details>
<summary><strong>9. Sacred Harvest Field</strong> — Growth: 14% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 60     | 30     | 15      | 105        |
| 2     | 68     | 34     | 17      | 119        |
| 3     | 77     | 38     | 19      | 134        |
| 5     | 101    | 50     | 25      | 176        |
| 10    | 195    | 97     | 48      | 340        |
| 15    | 375    | 187    | 93      | 655        |
| 20    | 723    | 361    | 180     | 1,264      |
| 25    | 1,392  | 696    | 348     | 2,436      |
| 30    | 2,681  | 1,340  | 670     | 4,691      |
| 35    | 5,163  | 2,581  | 1,290   | 9,034      |
| 40    | 9,941  | 4,970  | 2,485   | 17,396     |
</details>

<details>
<summary><strong>10. Watch Tower</strong> — Growth: 24% per level</summary>

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 50     | 110    | 70      | 230        |
| 2     | 62     | 136    | 86      | 284        |
| 3     | 76     | 169    | 107     | 352        |
| 5     | 118    | 260    | 165     | 543        |
| 10    | 346    | 762    | 485     | 1,593      |
| 15    | 1,015  | 2,235  | 1,422   | 4,672      |
| 20    | 2,978  | 6,552  | 4,169   | 13,699     |
</details>

---

#### Cost Progression — New Buildings

<details>
<summary><strong>11. Víðarr's Woodland</strong> — Growth: 14% per level</summary>

*Víðarr, the Silent God of forests and wilderness, blesses these ancient groves. Workers harvest timber under his watchful protection, and the deeper the woodland grows, the more bountiful the yield.*

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 30     | 50     | 20      | 100        |
| 2     | 34     | 56     | 22      | 112        |
| 3     | 38     | 64     | 25      | 127        |
| 4     | 44     | 74     | 29      | 147        |
| 5     | 50     | 84     | 33      | 167        |
| 10    | 97     | 162    | 65      | 324        |
| 15    | 187    | 313    | 125     | 625        |
| 20    | 361    | 602    | 241     | 1,204      |
| 25    | 696    | 1,160  | 464     | 2,320      |
| 30    | 1,340  | 2,234  | 893     | 4,467      |
| 35    | 2,581  | 4,302  | 1,721   | 8,604      |
| 40    | 4,970  | 8,284  | 3,313   | 16,567     |
</details>

<details>
<summary><strong>12. Dvergr's Quarry</strong> — Growth: 15% per level</summary>

*The Dvergr (Dwarves) — master craftsmen of the Nine Realms — carved these quarries deep into the mountainside. Their ancient techniques allow workers to extract stone with unmatched efficiency.*

| Level | 🪵 Wood | 🪨 Rock | ⛏️ Metal | Total Cost |
|:-----:|:------:|:------:|:-------:|:----------:|
| 1     | 55     | 35     | 25      | 115        |
| 2     | 63     | 40     | 28      | 131        |
| 3     | 72     | 46     | 33      | 151        |
| 4     | 83     | 53     | 38      | 174        |
| 5     | 96     | 61     | 43      | 200        |
| 10    | 193    | 123    | 87      | 403        |
| 15    | 389    | 247    | 176     | 812        |
| 20    | 782    | 498    | 355     | 1,635      |
| 25    | 1,574  | 1,001  | 715     | 3,290      |
| 30    | 3,166  | 2,015  | 1,439   | 6,620      |
| 35    | 6,369  | 4,053  | 2,895   | 13,317     |
| 40    | 12,810 | 8,152  | 5,823   | 26,785     |
</details>

---

#### Total Investment Cost Summary

| Building                 | Total Cost (all levels) | Growth/Level |
|--------------------------|:-----------------------:|:------------:|
| Berserker's Barrack      | 435,946                 | 18%          |
| Eldrunar's Cave          | 320,178                 | 15%          |
| Runic Sanctuary          | 271,771                 | 20%          |
| Dvergr's Quarry          | 204,542                 | 15%          |
| Sacred Harvest Field     | 140,858                 | 14%          |
| Víðarr's Woodland        | 134,145                 | 14%          |
| Jarl's Hall              | 133,409                 | 28%          |
| Drakkar Shipyard         | 96,002                  | 25%          |
| Freyja's Temple          | 73,700                  | 25%          |
| Watch Tower              | 69,799                  | 24%          |
| Storage & Market         | 49,949                  | 22%          |
| Explorer's House         | 5,101                   | 30%          |

> 💰 **Total investment to max a village: ~1,935,400 resources**

---

### 8.8 Citizens

#### Population Growth System

Population is the core workforce of your village. The **Sacred Harvest Field** determines your **maximum population capacity**. Each level adds an increasing number of citizens following an exponential curve:

```
PopulationGain(L) = floor( 5 × 1.08 ^ (L - 1) )
MaxPopulation(L) = BasePopulation + Σ PopulationGain(i),  for i = 1 to L
```

Where `BasePopulation = 30` (starting citizens).

---

#### Population Progression Table

<details>
<summary><strong>Sacred Harvest Field — Full Population Progression (Level 0–40)</strong></summary>

| Field Level | Pop Gain (this level) | Max Population |
|:-----------:|:---------------------:|:--------------:|
| 0           | —                     | 30             |
| 1           | +5                    | 35             |
| 2           | +5                    | 40             |
| 3           | +5                    | 45             |
| 4           | +6                    | 51             |
| 5           | +6                    | 57             |
| 6           | +7                    | 64             |
| 7           | +7                    | 71             |
| 8           | +8                    | 79             |
| 9           | +9                    | 88             |
| 10          | +9                    | 97             |
| 11          | +10                   | 107            |
| 12          | +11                   | 118            |
| 13          | +12                   | 130            |
| 14          | +13                   | 143            |
| 15          | +14                   | 157            |
| 16          | +15                   | 172            |
| 17          | +17                   | 189            |
| 18          | +18                   | 207            |
| 19          | +19                   | 226            |
| 20          | +21                   | 247            |
| 21          | +23                   | 270            |
| 22          | +25                   | 295            |
| 23          | +27                   | 322            |
| 24          | +29                   | 351            |
| 25          | +31                   | 382            |
| 26          | +34                   | 416            |
| 27          | +36                   | 452            |
| 28          | +39                   | 491            |
| 29          | +43                   | 534            |
| 30          | +46                   | 580            |
| 31          | +50                   | 630            |
| 32          | +54                   | 684            |
| 33          | +58                   | 742            |
| 34          | +63                   | 805            |
| 35          | +68                   | 873            |
| 36          | +73                   | 946            |
| 37          | +79                   | 1,025          |
| 38          | +86                   | 1,111          |
| 39          | +93                   | 1,204          |
| 40          | +100                  | **1,304**      |
</details>

> 👥 **Maximum Population: 1,304 citizens** (Sacred Harvest Field at level 40)

#### Key Population Milestones

| Milestone        | Field Level | Population |
|------------------|:-----------:|:----------:|
| Starting village | 0           | 30         |
| Early game       | 10          | 97         |
| Mid game         | 20          | 247        |
| Late game        | 30          | 580        |
| End game (max)   | 40          | 1,304      |

---

#### Population & Resource Collection System

Citizens can be **allocated to different tasks** to collect resources. The player defines what **percentage of the available population** is assigned to each activity.

#### Collection Rate Formula

```
ResourcePerHour(task) = AllocatedPopulation × BaseRatePerCitizen × BuildingMultiplier
```

Where:
- `AllocatedPopulation` = Total idle citizens × allocation % for that task
- `BaseRatePerCitizen` = fixed rate per citizen per hour (see table below)
- `BuildingMultiplier` = bonus from the corresponding source building level

---

#### Base Collection Rate (per 1 citizen, per hour)

| Resource           | Base Rate / Citizen / Hour | Source Building        | Building Bonus Formula           |
|--------------------|:--------------------------:|------------------------|----------------------------------|
| 🪵 **Wood**        | 12 / hour                  | Víðarr's Woodland      | `1 + (WoodlandLevel × 0.04)`     |
| 🪨 **Rock**        | 10 / hour                  | Dvergr's Quarry        | `1 + (QuarryLevel × 0.04)`       |
| ⛏️ **Metal**       | 8 / hour                   | Eldrunar's Cave        | `1 + (CaveLevel × 0.04)`        |
| ✨ **Eldrunar**     | 2 / hour                   | Eldrunar's Cave        | `1 + (CaveLevel × 0.03)`        |
| 🥩 **Meat**        | 15 / hour                  | Sacred Harvest Field   | `1 + (FieldLevel × 0.05)`       |
| 🦌 **Animal Skin** | 5 / hour                   | *(Hunting — manual allocation)* | —                     |

> **Example:** 20 citizens on Wood with Woodland Level 25 → `20 × 12 × (1 + 25 × 0.04) = 20 × 12 × 2.00 = 480 Wood/hour`
>
> **Example:** 10 citizens on Metal with Cave Level 20 → `10 × 8 × (1 + 20 × 0.04) = 10 × 8 × 1.80 = 144 Metal/hour`

---

#### Building Multiplier Progression

<details>
<summary><strong>Víðarr's Woodland — Wood Bonus</strong></summary>

| Woodland Level | Wood Multiplier | Wood/citizen/h |
|:--------------:|:---------------:|:--------------:|
| 0              | 1.00×           | 12.0           |
| 5              | 1.20×           | 14.4           |
| 10             | 1.40×           | 16.8           |
| 15             | 1.60×           | 19.2           |
| 20             | 1.80×           | 21.6           |
| 25             | 2.00×           | 24.0           |
| 30             | 2.20×           | 26.4           |
| 35             | 2.40×           | 28.8           |
| 40             | 2.60×           | 31.2           |
</details>

<details>
<summary><strong>Dvergr's Quarry — Rock Bonus</strong></summary>

| Quarry Level | Rock Multiplier | Rock/citizen/h |
|:------------:|:---------------:|:--------------:|
| 0            | 1.00×           | 10.0           |
| 5            | 1.20×           | 12.0           |
| 10           | 1.40×           | 14.0           |
| 15           | 1.60×           | 16.0           |
| 20           | 1.80×           | 18.0           |
| 25           | 2.00×           | 20.0           |
| 30           | 2.20×           | 22.0           |
| 35           | 2.40×           | 24.0           |
| 40           | 2.60×           | 26.0           |
</details>

<details>
<summary><strong>Eldrunar's Cave — Metal & Eldrunar Bonus</strong></summary>

| Cave Level | Metal Multiplier | Eldrunar Multiplier | Metal/citizen/h | Eldrunar/citizen/h |
|:----------:|:----------------:|:-------------------:|:---------------:|:------------------:|
| 0          | 1.00×            | 1.00×               | 8.0             | 2.0                |
| 5          | 1.20×            | 1.15×               | 9.6             | 2.3                |
| 10         | 1.40×            | 1.30×               | 11.2            | 2.6                |
| 15         | 1.60×            | 1.45×               | 12.8            | 2.9                |
| 20         | 1.80×            | 1.60×               | 14.4            | 3.2                |
| 25         | 2.00×            | 1.75×               | 16.0            | 3.5                |
| 30         | 2.20×            | 1.90×               | 17.6            | 3.8                |
| 35         | 2.40×            | 2.05×               | 19.2            | 4.1                |
| 40         | 2.60×            | 2.20×               | 20.8            | 4.4                |
</details>

<details>
<summary><strong>Sacred Harvest Field — Meat Bonus</strong></summary>

| Field Level | Meat Multiplier | Meat/citizen/h |
|:-----------:|:---------------:|:--------------:|
| 0           | 1.00×           | 15.0           |
| 5           | 1.25×           | 18.8           |
| 10          | 1.50×           | 22.5           |
| 15          | 1.75×           | 26.3           |
| 20          | 2.00×           | 30.0           |
| 25          | 2.25×           | 33.8           |
| 30          | 2.50×           | 37.5           |
| 35          | 2.75×           | 41.3           |
| 40          | 3.00×           | 45.0           |
</details>

---

#### Max Production Scenario (All buildings max, 1,304 citizens)

With 1,304 citizens at max population, 10% resting (130 idle), **1,174 allocatable citizens**:

| Allocation Example (equal split) | Citizens | Resource/Hour |
|----------------------------------|:--------:|:-------------:|
| 🪵 Wood (Woodland 40, 2.60×)    | 235      | 7,332         |
| 🪨 Rock (Quarry 40, 2.60×)      | 235      | 6,110         |
| ⛏️ Metal (Cave 40, 2.60×)       | 234      | 4,867         |
| ✨ Eldrunar (Cave 40, 2.20×)     | 118      | 520           |
| 🥩 Meat (Field 40, 3.00×)       | 234      | 10,530        |
| 🦌 Hunting                       | 118      | 590           |

---

#### Population Allocation Rules

1. **Total allocation cannot exceed 100%** of available (idle) citizens.
2. Citizens assigned to **buildings** (construction) or **troops** are not available for resource collection.
3. A minimum of **10% of total population** must remain unallocated (resting) to avoid **Morale penalties**.
4. **Diplomacy allocation**: Citizens can also be allocated to diplomacy tasks (trade caravans, cultural influence) — this uses the same pool.
5. Population growth requires sustained **Meat production** — if Meat reaches 0, population starts to decline.

> **Design Note:** This system creates meaningful choices: more citizens on Wood means fewer on Metal. Players must optimize allocation based on their current upgrade priorities and military needs.

---

### 8.9 Troops

Troops are trained at the **Berserker's Barrack** (regular troops) or **Freyja's Temple** (mythological troops). Each troop belongs to one of four tiers, has a combat role, and fits into a counter system that rewards strategic army composition.

> For detailed battle calculations, see [Battle Algorithm](./Battle%20Algorithm.md).

---

#### Troop Roster Overview

| #  | Name              | Tier          | Locomotion      | Attack Style    | Role            | Training Building       |
|:--:|-------------------|:-------------:|:---------------:|:---------------:|:---------------:|:-----------------------:|
| 1  | Spearman          | 🟢 Basic      | 🚶 Ground       | ⚔️ Melee        | 🛡️ Defensive    | Berserker's Barrack     |
| 2  | Viking            | 🟢 Basic      | 🚶 Ground       | ⚔️ Melee        | ⚔️ Offensive    | Berserker's Barrack     |
| 3  | Bowman            | 🟢 Basic      | 🚶 Ground       | 🏹 Ranged       | ⚔️ Offensive    | Berserker's Barrack     |
| 4  | Shieldmaiden      | 🔵 Advanced   | 🚶 Ground       | ⚔️ Melee        | 🛡️ Defensive    | Berserker's Barrack     |
| 5  | Berserker         | 🔵 Advanced   | 🚶 Ground       | ⚔️ Melee        | ⚔️ Offensive    | Berserker's Barrack     |
| 6  | Huntsman          | 🔵 Advanced   | 🚶 Ground       | 🏹 Ranged       | ⚔️ Offensive    | Berserker's Barrack     |
| 7  | Cavaleiro         | 🔵 Advanced   | 🚶 Ground       | ⚔️ Melee        | ⚔️🛡️ Mixed     | Berserker's Barrack     |
| 8  | Huskarl           | 🟣 Elite      | 🚶 Ground       | ⚔️ Melee        | 🛡️ Defensive    | Berserker's Barrack     |
| 9  | Jomsviking        | 🟣 Elite      | 🚶 Ground       | ⚔️ Melee        | ⚔️ Offensive    | Berserker's Barrack     |
| 10 | Ulfhednar         | 🟣 Elite      | 🚶 Ground       | ⚔️ Melee        | ⚔️ Offensive    | Berserker's Barrack     |
| 11 | Runecaster        | 🟣 Elite      | 🚶 Ground       | 🏹 Ranged       | ⚔️🛡️ Mixed     | Berserker's Barrack     |
| 12 | Valkyrie          | 🟡 Mythological | 🦅 Flying     | ⚔️ Melee        | ⚔️🛡️ Mixed     | Freyja's Temple         |
| 13 | Frost Giant       | 🟡 Mythological | 🚶 Ground     | ⚔️ Melee        | ⚔️ Offensive    | Freyja's Temple         |
| 14 | Raven of Odin     | 🟡 Mythological | 🦅 Flying     | 🏹 Ranged       | ⚔️ Offensive    | Freyja's Temple         |
| 15 | Einherjar         | 🟡 Mythological | 🚶 Ground     | ⚔️ Melee        | ⚔️🛡️ Mixed     | Freyja's Temple         |

---

#### Combat Stats

> ⚠️ These values are **initial proposals for playtesting**. Final balancing depends on simulation and testing.

| #  | Troop          | ATK | DEF (Melee) | DEF (Ranged) | Speed | Carry | Pop |
|:--:|----------------|:---:|:-----------:|:------------:|:-----:|:-----:|:---:|
| 1  | Spearman       | 10  | 25          | 10           | 18    | 25    | 1   |
| 2  | Viking         | 25  | 5           | 5            | 18    | 40    | 1   |
| 3  | Bowman         | 15  | 5           | 20           | 18    | 20    | 1   |
| 4  | Shieldmaiden   | 15  | 40          | 35           | 16    | 15    | 2   |
| 5  | Berserker      | 45  | 5           | 3            | 20    | 30    | 2   |
| 6  | Huntsman       | 30  | 8           | 20           | 22    | 30    | 2   |
| 7  | Cavaleiro      | 30  | 25          | 18           | 24    | 50    | 3   |
| 8  | Huskarl        | 20  | 55          | 45           | 14    | 15    | 5   |
| 9  | Jomsviking     | 60  | 20          | 12           | 16    | 40    | 5   |
| 10 | Ulfhednar      | 55  | 10          | 8            | 28    | 60    | 4   |
| 11 | Runecaster     | 35  | 15          | 25           | 14    | 20    | 5   |
| 12 | Valkyrie       | 70  | 40          | 30           | 30    | 30    | 10  |
| 13 | Frost Giant    | 120 | 50          | 15           | 8     | 100   | 15  |
| 14 | Raven of Odin  | 50  | 10          | 10           | 35    | 10    | 8   |
| 15 | Einherjar      | 90  | 60          | 50           | 20    | 50    | 12  |

- **ATK** — Offensive power per unit
- **DEF (Melee)** — Defense against melee attacks
- **DEF (Ranged)** — Defense against ranged attacks
- **Speed** — Movement speed on the world map (tiles/hour)
- **Carry** — Maximum resources the unit can loot per trip
- **Pop** — Citizens consumed to train one unit

> **TODO:** Define training costs (Wood, Rock, Metal, Eldrunar, Meat, Skin) and training times for each troop.

---

#### Tier Requirements

| Tier | Pop Cost | Typical Requirements |
|------|:--------:|----------------------|
| 🟢 **Basic** | 1 | Berserker's Barrack Level 1–5 |
| 🔵 **Advanced** | 2–3 | Berserker's Barrack Level 10–20 + Runic Sanctuary Research |
| 🟣 **Elite** | 4–6 | Berserker's Barrack Level 25–35 + Advanced Research |
| 🟡 **Mythological** | 8–15 | Freyja's Temple + Deity Allegiance + Eldrunar |

---

#### Troop Visual Descriptions

<details>
<summary><strong>🟢 Basic Troops — Visual Reference</strong></summary>

##### Spearman
**Silhouette:** Medium-height, lean build. Stands upright with spear held vertically, round wooden shield at the side.
**Armor:** Leather tunic over wool clothing. Simple iron nasal helm. Wrapped leather bracers on forearms.
**Weapons:** 2-meter ash-wood spear with iron tip. Round shield (linden wood, iron boss center, painted in clan colors).
**Color Palette:** Earth tones — brown leather, grey wool, weathered wood. Shield features a single clan rune mark.
**Visual Identity:** The "everyman" soldier. Looks like a farmer who picked up a spear. Reliable, unglamorous, numerous.

##### Viking
**Silhouette:** Broad-shouldered, aggressive forward lean. Leading arm raised with axe. No shield — both hands weapon-ready.
**Armor:** Bare arms with runic tattoos. Sleeveless leather vest over a linen shirt. Thick leather belt with iron buckle. Simple leather skullcap or no helm.
**Weapons:** Single-handed bearded axe (primary). Seax knife at the belt (backup).
**Color Palette:** Raw, primal — exposed skin with blue-black tattoo ink, dark leather, iron-grey blade edge. Hair is wild and unkempt.
**Visual Identity:** Feral energy. Should look like someone who WANTS to fight. Eyes wide, muscles tense. The quintessential Norse raider.

##### Bowman
**Silhouette:** Tall, lean, slightly crouched in a ready stance. Bow drawn at half-tension. Quiver visible on the back.
**Armor:** Hooded cloak over light leather armor. Fur-lined boots for silent movement. Leather finger-guards.
**Weapons:** Longbow made of yew wood. Quiver of 20 iron-tipped arrows. Hunting knife at the hip.
**Color Palette:** Forest camouflage — dark green cloak, brown leather, muted tones. Designed to blend into the Midgard woodlands.
**Visual Identity:** The silent hunter. Calm, focused posture. Should feel like they appeared out of nowhere.

</details>

<details>
<summary><strong>🔵 Advanced Troops — Visual Reference</strong></summary>

##### Shieldmaiden
**Silhouette:** Strong, planted stance. Oversized shield held forward, creating a wall effect. Short sword visible behind the shield.
**Armor:** Chainmail byrnie over padded wool gambeson. Iron-rimmed round shield (larger than Spearman's). Open-faced iron helm with cheek guards. Braided hair tucked under a leather headband.
**Weapons:** Reinforced iron-rimmed shield (90cm diameter, iron boss, riveted construction). Short sword / gladius-length blade for close-quarters.
**Color Palette:** Iron grey chainmail, burnished shield surface with intricate knotwork patterns. Strong feminine features visible. Blue cloth accents.
**Visual Identity:** Powerful and dignified. Should evoke Brynhildr and Lagertha — not sexualized, but clearly formidable. The shield dominates her visual profile.

##### Berserker
**Silhouette:** Hunched, feral crouch. Arms spread wide, dual weapons ready. Mouth open in a scream. Spine curved forward like a predatory animal.
**Armor:** Bare-chested with ritual scarification and extensive blue-black tattoos (runic protection symbols). Bear-skin pelt draped over one shoulder. Leather trousers bound with rope.
**Weapons:** Dual bearded axes or a two-handed Dane axe. No shield (disdains defense).
**Color Palette:** Exposed flesh is the primary "color" — scars, tattoos, and sweat. The bear pelt is dark brown. Axes are blackened iron. Eyes are wild, pupils dilated.
**Visual Identity:** Terror incarnate. Should make the viewer uncomfortable. The antithesis of the disciplined Shieldmaiden — pure, unhinged violence.

##### Huntsman
**Silhouette:** Low, crouched stance — almost kneeling. Compact bow drawn with precision. Cloak draped to break the silhouette.
**Armor:** Reinforced leather armor with metal studs. Wolfskin cloak with hood pulled forward, obscuring the face. Light leather boots with wrapped ankle supports.
**Weapons:** Composite recurve bow (shorter than Bowman's longbow but more powerful). Runic-tipped arrows with distinctive fletching. A hatchet at the belt for emergency melee.
**Color Palette:** Deep forest — charcoal grey cloak, dark brown leather, hints of dark green. The runic arrow tips glow faintly with a pale blue shimmer.
**Visual Identity:** Predator, not a soldier. Where the Bowman is a trained militiaman, the Huntsman is an apex predator who has survived the wilds alone.

##### Cavaleiro (Riddari)
**Silhouette:** Mounted figure on a stocky Norse horse. Sits tall in the saddle. Lance or sword raised. Horse draped with a simple cloth caparison.
**Armor:** Half-plate over chainmail — iron pauldrons, breastplate, chainmail skirt. Open-faced helm with a nose guard and bronze inlay. Leather riding gloves.
**Weapons:** Mounted lance (primary charge weapon). Arming sword (secondary). Round shield strapped to the off-arm or saddle.
**Color Palette:** The most "knightly" troop — polished iron with bronze accents, deep crimson cloth caparison on the horse, clan sigil on the shield. The horse is a sturdy fjord breed (grey-dun color).
**Visual Identity:** Authority and versatility. The Jarl's trusted commander. Should feel like the most "professional" soldier on the battlefield — disciplined, well-equipped, deadly from horseback.

</details>

<details>
<summary><strong>🟣 Elite Troops — Visual Reference</strong></summary>

##### Huskarl
**Silhouette:** Massive, immovable wall. Tower shield planted on the ground, only the eyes visible above. Full body concealed behind metal and wood.
**Armor:** Full chainmail hauberk extending to the knees. Steel lamellar over the torso. Tower shield (kite-shaped, reinforced with iron bands). Full enclosed iron helm with eye-slits.
**Weapons:** Long sword (for reach around the shield). Tower shield is the primary "weapon" — a wall of defense.
**Color Palette:** All iron and steel — the darkest, most heavily-armored silhouette. The helm obscures all humanity. Gold trim on the shield edge indicates Jarl's personal guard. Deep purple cloth beneath the armor.
**Visual Identity:** An impenetrable fortress in human form. No face visible, no personality shown — only duty and iron. The ultimate "I shall not move" energy.

##### Jomsviking
**Silhouette:** Tall, athletic, combat-ready stance. Balanced posture — equally ready for offense or defense. Dual-wielding or wielding a longsword with shield.
**Armor:** Blackened chainmail with a leather surcoat bearing the Jomsviking raven insignia. Iron-reinforced bracers. Open-faced helm with raven-wing side pieces. Black cloak pinned at the shoulder.
**Weapons:** Longsword (masterwork steel). Round battle shield with the Jomsviking brotherhood emblem — a raven with spread wings over a sword.
**Color Palette:** Black and gold — blackened iron, black leather, black cloak, with gold raven insignia and gold sword pommel. The elite mercenary look. Clean, professional, lethal.
**Visual Identity:** The "special forces" aesthetic. Every piece of gear is perfectly maintained. They move like professionals. The contrast with the feral Berserker is stark — same destructive power, but with precision and control.

##### Ulfhednar (Wolf-Warrior)
**Silhouette:** Low, feral sprint posture. Running on all fours or in a deep crouch. Wolf pelt over the head and shoulders, blending the human and animal silhouette.
**Armor:** Almost none — wolfskin pelt from head to waist. Leather wraps on forearms and shins. Ritual wolf-tooth necklace. Body marked with wolf-blood rune paint (temporary war paint).
**Weapons:** Two hand-axes (fast, dual-wielded). Teeth filed to points. Claws? Maybe ritualistic iron claw-gauntlets.
**Color Palette:** Grey wolf fur is the dominant color. Pale skin beneath, smeared with dark grey war paint in wolf-rune patterns. Iron-grey axes. Yellow eyes (contact lenses? ritual transformation?).
**Visual Identity:** Half-man, half-wolf. Should be genuinely unsettling — faster than a Berserker, more animalistic. The wolf pelt should make it ambiguous whether this is a person or a creature.

##### Runecaster
**Silhouette:** Upright, mystical stance. One hand extended forward with glowing runes spiraling around the fingers. Staff held in the off-hand. Robes flowing.
**Armor:** Runic-inscribed leather armor beneath layered wool and linen robes. Iron circlet with an Eldrunar crystal embedded in the forehead. Leather bandoliers holding runic stones.
**Weapons:** Runic staff (channeling focus, not melee). Runic stones thrown as projectiles — they glow pale blue/purple and shatter on impact, releasing energy. A ritual dagger at the belt.
**Color Palette:** Deep purple robes with silver-blue runic embroidery. The Eldrunar crystal glows faintly turquoise. Rune stones emit a pale blue light. The overall impression is "warrior-mage" — armored enough for the battlefield, mystical enough to feel otherworldly.
**Visual Identity:** The transition point between human and divine. Should look scholarly but dangerous. Runes floating in the air around them during combat as a VFX signature.

</details>

<details>
<summary><strong>🟡 Mythological Troops — Visual Reference</strong></summary>

##### Valkyrie
**Silhouette:** Winged warrior descending from above. Large feathered wings (swan or eagle) spread wide. Spear pointed downward in a diving attack posture. Hair flowing behind.
**Armor:** Golden-white chainmail that seems to glow softly. Winged helm (actual functioning wings, not decorative). Skirt of chainmail links extending to the knees. Bracers inscribed with Freyja's runes.
**Weapons:** Divine spear (Gungnir-inspired, but shorter — a javelin/spear hybrid). Small round shield with Freyja's cat sigil. A secondary short sword at the hip.
**Color Palette:** Celestial — gold, white, and pale blue. The armor catches light unnaturally. Wings are white-grey feathered with golden tips. Eyes glow with a faint amber light. Golden hair (or silver).
**Visual Identity:** Angelic yet militant. Beautiful but terrifying — like a divine predator descending on prey. The wings should be functional and large (3-4m wingspan). Should feel like encountering something from another world entirely.

##### Frost Giant (Jötunn)
**Silhouette:** Enormous — 4x the height of a human troop. Hunched, brutish posture. Massive arms that drag near the ground. Ice crystals forming on the body.
**Armor:** No crafted armor — the Jötunn's skin IS the armor. Blue-grey stone-like skin covered in patches of ice and frost. Ancient runes carved directly into the flesh (Skadi's marks). A crude loincloth of mammoth hide.
**Weapons:** An uprooted tree trunk used as a club, or a boulder carried in one hand for throwing. Some may carry a crude ice-axe the size of a human door.
**Color Palette:** Arctic blues and greys — pale blue skin, white frost patches, grey-blue ice formations. Eyes are deep glacial blue with no visible pupil. Breath is visible fog. The ground freezes where they step.
**Visual Identity:** Primordial, ancient, MASSIVE. Should dwarf everything on screen. Not evil, but elemental — a force of nature that has been weaponized. Ice particles fall from them constantly.

##### Raven of Odin (Huginn)
**Silhouette:** Oversized raven (2m wingspan) surrounded by a faint aura of dark energy. Talons extended. Beak open with visible runic energy crackling inside.
**Armor:** No traditional armor — the feathers themselves shimmer with a dark iridescent magical shield. A small runic collar around the neck (binding sigil from Odin).
**Weapons:** Razor talons crackling with runic lightning. Arcane bolts fired from the beak (ranged attack). The wingbeat itself creates small shockwaves.
**Color Palette:** Midnight black feathers with iridescent purple-blue sheen (like oil on water). Eyes are pure white with no iris. Runic energy is dark purple. The collar is gold with a single Eldrunar crystal.
**Visual Identity:** Ominous intelligence. This is not a bird — it's a divine agent. Should feel like being watched by something ancient and omniscient. The glow from its eyes should be unsettling.

##### Einherjar
**Silhouette:** Spectral warrior with an ethereal, semi-transparent quality. A perfect warrior form — ideal proportions, perfect posture, weapons held with absolute mastery. Faint golden light emanates from within.
**Armor:** Ancient ornate armor that blends Norse styles from across centuries — elements of Viking, Anglo-Saxon, and proto-Germanic design. The armor appears solid but occasionally flickers with transparency, revealing golden light beneath. Helm with eye-guards, full chainmail, ornate bracers.
**Weapons:** A masterwork sword that glows with Tyr's golden rune on the blade. A round shield with an embossed design of Valhalla's gates. The weapons appear to be made of light as much as steel.
**Color Palette:** Gold, spectral white, and ethereal blue. The entire figure has a faint golden luminescence. The armor is bronze-gold with ancient green patina in the crevices. Eyes are solid gold. When they move, afterimages trail behind them briefly.
**Visual Identity:** The perfected warrior ideal. Every movement is precise, every stance is textbook. They are what every living warrior aspires to become — the absolute pinnacle of combat, transcending death itself. Should feel simultaneously inspiring and melancholic — these are the *dead*.

</details>

---

#### Troop Descriptions

<details>
<summary><strong>🟢 Tier 1 — Basic Troops</strong></summary>

##### 1. Spearman *(Lanceiro)*

**Type:** Ground · Melee · Defensive
**Tier:** 🟢 Basic

The backbone of any Norse settlement's defense. Armed with a sturdy ash-wood spear and a round shield, the Spearman excels at holding defensive lines and repelling mounted charges. Their long reach gives them a natural advantage against cavalry, but they struggle against fast-moving melee fighters who can get past their spear point. Every Jarl's first line of defense begins with a wall of spears.

- **Strong against:** Cavaleiro, mounted units
- **Weak against:** Viking, Berserker (aggressive melee infantry)

---

##### 2. Viking

**Type:** Ground · Melee · Offensive
**Tier:** 🟢 Basic

The fearsome Norse raider — armed with a battle-axe and burning fury. The Viking embodies raw offensive power: devastating melee damage at the cost of minimal armor and self-preservation. They believe that death in battle is the gateway to Valhalla, and they fight accordingly — charging headfirst without hesitation. Their aggression makes them exceptional against ranged units who cannot withstand close combat, but disciplined defensive formations can break their charge.

- **Strong against:** Bowman, ranged units, light troops
- **Weak against:** Spearman, Shieldmaiden (disciplined defensive troops)

---

##### 3. Bowman *(Arqueiro)*

**Type:** Ground · Ranged · Offensive
**Tier:** 🟢 Basic

Hunters and woodsmen trained to loose arrows with deadly precision. The Bowman strikes from a distance, whittling down enemy forces before the melee clash begins. Effective against slow-moving heavy infantry and siege formations, but fatally vulnerable once enemies close the gap. In the frozen forests of Midgard, a good hunter learns patience — and patience wins wars.

- **Strong against:** Slow infantry, heavy troops, siege
- **Weak against:** Viking, Cavaleiro (fast melee closers)

</details>

<details>
<summary><strong>🔵 Tier 2 — Advanced Troops</strong></summary>

##### 4. Shieldmaiden *(Donzela do Escudo)*

**Type:** Ground · Melee · Defensive
**Tier:** 🔵 Advanced

Legendary warrior women of the North, the Shieldmaidens wield reinforced iron-rimmed shields and short swords forged in the Dvergr's Quarry. They form impenetrable shield walls that absorb enemy charges and arrow volleys alike — boasting the highest combined melee and ranged defense among non-elite troops. Inspired by the sagas of Lagertha and Brynhildr, they are the living walls that protect what matters most.

- **Strong against:** Bowman, Huntsman, ranged attackers
- **Weak against:** Berserker, Jomsviking (overwhelming offensive power)

---

##### 5. Berserker

**Type:** Ground · Melee · Offensive
**Tier:** 🔵 Advanced

Warriors consumed by the divine battle-trance known as *berserksgangr*. When the sacred fury takes hold, a Berserker becomes an unstoppable force of destruction — the highest melee damage output among all human troops. They fight shirtless, biting their shields, impervious to pain and fear. But the trance offers no protection: their defense is virtually nonexistent. A Berserker kills or dies. There is no middle ground.

- **Strong against:** Shieldmaiden, defensive formations (overwhelming force)
- **Weak against:** Spearman, Huntsman (disciplined counters)

---

##### 6. Huntsman *(Rastreador)*

**Type:** Ground · Ranged · Offensive
**Tier:** 🔵 Advanced

Elite trackers and marksmen who have spent years surviving the untamed wilds of Midgard. Armed with composite bows and runic-tipped arrows, the Huntsman is deadlier and more precise than the common Bowman. Their arrows are specifically designed to pierce heavy armor, granting them a significant damage bonus against slow, armored targets like Huskarls and shield formations. Silent, patient, lethal.

- **Strong against:** Heavy/slow troops (Huskarl, Shieldmaiden, Frost Giant)
- **Weak against:** Viking, Cavaleiro (fast melee engagement)

---

##### 7. Cavaleiro *(Riddari)*

**Type:** Ground · Melee · Mixed (Offensive + Defensive)
**Tier:** 🔵 Advanced

Mounted warriors riding the sturdy war-horses bred in the highlands of Midgard. The Cavaleiro is the most versatile unit in any Jarl's arsenal — capable of both devastating charges and reliable defensive maneuvers. They are not the strongest attacker nor the toughest defender, but they are competent at both, making them invaluable when the battle situation is uncertain. The cavalry of the Jarls has turned the tide of countless battles.

- **Strong against:** Bowman, Huntsman, ranged units (fast gap-closing)
- **Weak against:** Spearman, Huskarl (anti-cavalry specialists)

</details>

<details>
<summary><strong>🟣 Tier 3 — Elite Troops</strong></summary>

##### 8. Huskarl

**Type:** Ground · Melee · Defensive
**Tier:** 🟣 Elite

The Jarl's personal guard — elite warriors clad in heavy iron armor, carrying tower shields and long swords. The Huskarl is the most resilient defensive unit in the entire game: a living wall of steel and unwavering loyalty. They require enormous amounts of Metal and Animal Skin to equip, and each one consumes significant population. But behind a line of Huskarls, a village stands unconquerable. Their oath is simple: *"The Jarl falls only after the last Huskarl has fallen."*

- **Strong against:** Almost all ground troops (superior defense absorbs damage)
- **Weak against:** Berserker, Jomsviking (raw damage that bypasses defensive posture)

---

##### 9. Jomsviking

**Type:** Ground · Melee · Offensive
**Tier:** 🟣 Elite

Members of the legendary Jomsviking brotherhood — the most feared mercenary warriors in all of the North. Bound by an iron code of honor and forged in relentless combat, they combine devastating attack power with respectable defense. The Jomsviking is the definitive ground offensive unit: capable of breaking any formation, piercing any defense, and still standing when the dust settles. Recruiting them requires wealth and reputation.

- **Strong against:** Huskarl, defensive formations, fortified positions
- **Weak against:** Huntsman, Runecaster (ranged kiting and debuffs)

---

##### 10. Ulfhednar *(Wolf-Warrior)*

**Type:** Ground · Melee · Offensive
**Tier:** 🟣 Elite

A step beyond the Berserker — the Ulfhednar dons the pelt of a sacred wolf and channels the beast's spirit in battle. Faster on the world map than any other ground troop, with raw damage rivaling the Jomsviking. The Ulfhednar is purpose-built for lightning raids: strike fast, loot everything, vanish before reinforcements arrive. Their speed makes them the ultimate harassment and pillaging unit, but they share the Berserker's weakness — charging into a prepared defensive formation is suicide.

- **Strong against:** Isolated targets, resource raids, undefended villages
- **Weak against:** Prepared defenses, Shieldmaiden + Spearman formations

---

##### 11. Runecaster *(Lançador de Runas)*

**Type:** Ground · Ranged · Mixed (Offensive + Defensive)
**Tier:** 🟣 Elite

Warrior-mages who have studied the ancient runes at the Runic Sanctuary. They hurl projectiles infused with runic energy — not as powerful as a Huntsman's arrows, but carrying supernatural effects that weaken enemy troops (reduced accuracy, slowed movement, or diminished morale). The Runecaster is the bridge between the mundane and the divine, and the only regular troop that requires **Eldrunar** to train. They are also one of the few ground units capable of engaging flying enemies.

- **Strong against:** Heavy troops (debuffs), flying units (runic tracking)
- **Weak against:** Viking, Cavaleiro (fast melee closing negates range)

</details>

<details>
<summary><strong>🟡 Tier 4 — Mythological Troops</strong></summary>

> ⚠️ Mythological troops require allegiance to a **specific deity** at **Freyja's Temple** and consume **Eldrunar** to train. They are rare, powerful, and game-changing.

##### 12. Valkyrie

**Type:** Flying · Melee · Mixed (Offensive + Defensive)
**Tier:** 🟡 Mythological
**Deity:** Freyja

The chosen warriors of Freyja, descended from the heavens on wings of light. Valkyries are **flying melee combatants** — they dive from above to strike with spear and sword, then ascend beyond the reach of ground-based melee troops. Their balanced attack and defense make them formidable in any engagement. However, they can be brought down by ranged units who can track their flight. Freyja sends her Valkyries to carry the worthy to Fólkvangr — and to ensure the unworthy never arrive.

- **Strong against:** All ground melee troops (immune to melee counterattack)
- **Weak against:** Bowman, Huntsman, Runecaster (ranged fire)

---

##### 13. Frost Giant *(Jötunn)*

**Type:** Ground · Melee · Offensive (Siege)
**Tier:** 🟡 Mythological
**Deity:** Skadi

A towering Jötunn summoned from the frozen peaks of Jötunheimr by Skadi's blessing. Slow, immense, and catastrophically powerful. The Frost Giant functions as a **living siege engine** — capable of smashing walls, crushing formations, and dealing devastating area damage. Each one consumes enormous population and Eldrunar to summon, and their glacial speed makes them vulnerable to ranged harassment. But when a Frost Giant reaches the gates, the gates cease to exist.

- **Strong against:** Walls, fortifications, clustered formations, defensive lines
- **Weak against:** Ranged units (Huntsman, Runecaster), fast raiders (Ulfhednar)

---

##### 14. Raven of Odin *(Huginn)*

**Type:** Flying · Ranged · Offensive
**Tier:** 🟡 Mythological
**Deity:** —  *(Available once Freyja's Temple reaches sufficient level)*

Enchanted ravens imbued with runic energy, striking from above with razor talons and arcane bolts. The Raven of Odin is a **flying ranged harasser** — fragile but untouchable by ground melee troops. They excel at picking off isolated units and disrupting enemy formations from above. Their weakness is their fragility: a focused volley from ground archers or a Runecaster's tracking projectiles will bring them down. Odin's eyes see all, and his ravens deliver his judgment.

- **Strong against:** Ground melee troops without ranged support
- **Weak against:** Huntsman, Runecaster, Bowman (concentrated ranged fire)

---

##### 15. Einherjar

**Type:** Ground · Melee · Mixed (Offensive + Defensive)
**Tier:** 🟡 Mythological
**Deity:** Tyr

The spirits of fallen warriors, summoned from Valhalla by Tyr's divine authority. The Einherjar is the **most powerful and balanced troop in the game** — exceptional attack, exceptional defense, exceptional HP. They are warriors who have already died and been perfected in the eternal training grounds of the gods. Summoning even a single Einherjar requires immense Eldrunar and population sacrifice. They are the ultimate investment: a small number of Einherjar can hold against armies. But they are not invincible — they can be overwhelmed by sheer numbers or outmaneuvered by faster raiders.

- **Strong against:** Almost everything in direct combat (supreme stats)
- **Weak against:** Numerical superiority, Ulfhednar raids (speed vs. power)

</details>

---

#### Counter System Overview

> The counter system ensures that **no single troop dominates** without a viable response. Every troop has strengths and weaknesses, creating strategic depth in army composition.

| Troop | Strong Against | Weak Against |
|-------|---------------|-------------- |
| Spearman | Cavaleiro, mounted units | Viking, Berserker |
| Viking | Bowman, ranged, light troops | Spearman, Shieldmaiden |
| Bowman | Slow infantry, heavy troops | Viking, Cavaleiro |
| Shieldmaiden | Ranged attackers | Berserker, Jomsviking |
| Berserker | Defensive formations | Spearman, Huntsman |
| Huntsman | Heavy/slow troops | Viking, Cavaleiro |
| Cavaleiro | Ranged units | Spearman, Huskarl |
| Huskarl | Most ground troops | Berserker, Jomsviking |
| Jomsviking | Defensive formations | Huntsman, Runecaster |
| Ulfhednar | Isolated targets, raids | Prepared defenses |
| Runecaster | Heavy troops, flying units | Fast melee closers |
| Valkyrie | Ground melee troops | Ranged fire |
| Frost Giant | Walls, fortifications | Ranged units, fast raiders |
| Raven of Odin | Ground melee without ranged | Concentrated ranged fire |
| Einherjar | Direct combat superiority | Numerical superiority |

---

#### Flying Unit Mechanics

> Flying troops (**Valkyrie**, **Raven of Odin**) introduce a new combat layer in the late game:
> - **Immune to ground melee attacks** — ground melee troops cannot target flying units.
> - **Vulnerable to ranged fire** — Bowman, Huntsman, and Runecaster can target flying units.
> - **Flying vs. Flying** — flying units can engage each other in melee or ranged combat.
> - **Strategic implication:** Late-game armies without ranged support become highly vulnerable to aerial assaults.

---

### 8.10 Heroes

> **TODO:** Define hero system. Consider a roster of **9 heroes** — one mandatory per specific area/realm. Balance is a key challenge.

---

### 8.11 Special Troops

Mythological troops are defined in **Section 8.9 — Tier 4 (Mythological)**. These troops are unlocked through the **Freyja's Temple** building and require:

- **Deity allegiance** — The player must be devoted to the corresponding deity (Freyja, Skadi, or Tyr).
- **Eldrunar** — Mythological troops consume Eldrunar as part of their training cost.
- **Freyja's Temple level** — Each mythological troop requires a minimum temple level.

| # | Troop | Deity | Special Trait |
|:-:|-------|:-----:|---------------|
| 12 | **Valkyrie** | Freyja | 🦅 Flying · Immune to ground melee |
| 13 | **Frost Giant** | Skadi | 🏰 Living siege engine · Area damage |
| 14 | **Raven of Odin** | — | 🦅 Flying · Ranged harasser |
| 15 | **Einherjar** | Tyr | 👑 Supreme balanced stats · Spirit warrior |

> **TODO:** Define special abilities, deity-specific bonuses, and Eldrunar costs for each mythological troop.

---

### 8.12 Ships

> **TODO:** Define ship types, stats, and naval mechanics.

---

### 8.13 Buff & Nerf Elements

> **TODO:** Define buff/nerf mechanics — deity bonuses, runic upgrades, environmental effects, etc.

---

### 8.14 Battles

Battles are **auto-resolved instantly** on the server using **Lanchester's Square Law**. The player makes strategic decisions before combat (army composition, target selection), and the algorithm determines the winner and losses.

> 📄 **Full algorithm with step-by-step formulas and 4 simulated battles:** [Battle Algorithm](./Battle%20Algorithm.md)

#### Core Formula

```
If ATK > DEF → Attacker WINS
  Attacker survival = √(1 − (DEF/ATK)²)
  All defending troops are destroyed.

If DEF ≥ ATK → Defender WINS
  Defender survival = √(1 − (ATK/DEF)²)
  All attacking troops are destroyed.
```

#### Key Mechanics

| Mechanic | Description |
|----------|-------------|
| **Weighted Defense** | Each defender uses `DEF(M)` or `DEF(R)` proportionally to the attacker's melee/ranged composition |
| **Flying Units** | Ground melee troops cannot target flying units; only ranged defenders contribute defense against aerial attacks |
| **Watch Tower** | Multiplies total defense by up to 2.00× at level 20, plus adds flat base defense |
| **Luck Factor** | Optional ±10% random modifier to prevent 100% predictable outcomes |

#### Watch Tower Defense Bonus

```
wall_modifier = 1 + (WatchTowerLevel × 0.05)
base_wall_DEF = WatchTowerLevel × 50
Total_DEF = (DEF_base × wall_modifier) + base_wall_DEF
```

| Watch Tower Level | Multiplier | Base DEF |
|:-----------------:|:----------:|:--------:|
| 0 | 1.00× | 0 |
| 5 | 1.25× | 250 |
| 10 | 1.50× | 500 |
| 15 | 1.75× | 750 |
| 20 | 2.00× | 1,000 |

#### Loss Curve Behavior

| Ratio (loser/winner) | Winner Survival | Winner Losses |
|:--------------------:|:---------------:|:-------------:|
| 0.10 (domination) | 99.5% | 0.5% |
| 0.50 | 86.6% | 13.4% |
| 0.80 | 60.0% | 40.0% |
| 0.95 | 31.2% | 68.8% |
| 1.00 (tie) | 0.0% | 100% |

> **Design Implication:** Attacking with only a slight advantage results in catastrophic losses. Players are incentivized to attack with clear superiority or to use optimized compositions that exploit the counter system.

---

### 8.15 Points & Ranking

#### Scoring Algorithm

Village points are calculated based on building levels. Each building level grants an incremental amount of points following an **exponential growth** formula:

```
PointsGainedAtLevel(L) = floor( BasePoints × GrowthRate ^ (L - 1) )
```

```
TotalBuildingPoints(L) = Σ PointsGainedAtLevel(i),  for i = 1 to L
```

```
VillageScore = Σ TotalBuildingPoints(MaxLevel_b),  for each building b
```

> **Design Rationale:** Higher-importance buildings have higher `BasePoints`. Buildings with more levels use a slower `GrowthRate` to prevent them from dominating the total score. This ensures that upgrading any building is always meaningful, while avoiding extreme point inflation at high levels.

---

#### Building Parameters

| #  | Building                 | Base Points | Growth Rate | Max Level | Max Points |
|----|--------------------------|:-----------:|:-----------:|:---------:|:----------:|
| 1  | Jarl's Hall              | 17          | 1.12        | 20        | **1,217**  |
| 2  | Freyja's Temple          | 12          | 1.12        | 20        | **855**    |
| 3  | Berserker's Barrack      | 10          | 1.07        | 35        | **1,370**  |
| 4  | Drakkar Shipyard         | 12          | 1.12        | 20        | **855**    |
| 5  | Explorer's House         | 12          | 1.20        | 10        | **305**    |
| 6  | Eldrunar's Cave          | 8           | 1.06        | 40        | **1,221**  |
| 7  | Runic Sanctuary          | 10          | 1.08        | 30        | **1,118**  |
| 8  | Storage & Market         | 10          | 1.12        | 20        | **710**    |
| 9  | Sacred Harvest Field     | 7           | 1.06        | 40        | **1,061**  |
| 10 | Watch Tower              | 11          | 1.12        | 20        | **783**    |
| 11 | Víðarr's Woodland        | 6           | 1.06        | 40        | **911**    |
| 12 | Dvergr's Quarry          | 6           | 1.06        | 40        | **911**    |
|    |                          |             |             | **TOTAL** | **11,317** |

> 🏰 **Maximum Village Score: 11,317 points** (all 12 buildings at max level)

---

#### Point Progression Tables

<details>
<summary><strong>1. Jarl's Hall</strong> — Base: 17 | Growth: 1.12 | Max: 20 | Total: 1,217 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 17                  | 17                 |
| 2     | 19                  | 36                 |
| 3     | 21                  | 57                 |
| 4     | 23                  | 80                 |
| 5     | 26                  | 106                |
| 6     | 29                  | 135                |
| 7     | 33                  | 168                |
| 8     | 37                  | 205                |
| 9     | 42                  | 247                |
| 10    | 47                  | 294                |
| 11    | 52                  | 346                |
| 12    | 59                  | 405                |
| 13    | 66                  | 471                |
| 14    | 74                  | 545                |
| 15    | 83                  | 628                |
| 16    | 93                  | 721                |
| 17    | 104                 | 825                |
| 18    | 116                 | 941                |
| 19    | 130                 | 1,071              |
| 20    | 146                 | **1,217**          |
</details>

<details>
<summary><strong>2. Freyja's Temple</strong> — Base: 12 | Growth: 1.12 | Max: 20 | Total: 855 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 12                  | 12                 |
| 2     | 13                  | 25                 |
| 3     | 15                  | 40                 |
| 4     | 16                  | 56                 |
| 5     | 18                  | 74                 |
| 6     | 21                  | 95                 |
| 7     | 23                  | 118                |
| 8     | 26                  | 144                |
| 9     | 29                  | 173                |
| 10    | 33                  | 206                |
| 11    | 37                  | 243                |
| 12    | 41                  | 284                |
| 13    | 46                  | 330                |
| 14    | 52                  | 382                |
| 15    | 58                  | 440                |
| 16    | 65                  | 505                |
| 17    | 73                  | 578                |
| 18    | 82                  | 660                |
| 19    | 92                  | 752                |
| 20    | 103                 | **855**            |
</details>

<details>
<summary><strong>3. Berserker's Barrack</strong> — Base: 10 | Growth: 1.07 | Max: 35 | Total: 1,370 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 10                  | 10                 |
| 2     | 10                  | 20                 |
| 3     | 11                  | 31                 |
| 4     | 12                  | 43                 |
| 5     | 13                  | 56                 |
| 6     | 14                  | 70                 |
| 7     | 15                  | 85                 |
| 8     | 16                  | 101                |
| 9     | 17                  | 118                |
| 10    | 18                  | 136                |
| 11    | 19                  | 155                |
| 12    | 21                  | 176                |
| 13    | 22                  | 198                |
| 14    | 24                  | 222                |
| 15    | 25                  | 247                |
| 16    | 27                  | 274                |
| 17    | 29                  | 303                |
| 18    | 31                  | 334                |
| 19    | 33                  | 367                |
| 20    | 36                  | 403                |
| 21    | 38                  | 441                |
| 22    | 41                  | 482                |
| 23    | 44                  | 526                |
| 24    | 47                  | 573                |
| 25    | 50                  | 623                |
| 26    | 54                  | 677                |
| 27    | 58                  | 735                |
| 28    | 62                  | 797                |
| 29    | 66                  | 863                |
| 30    | 71                  | 934                |
| 31    | 76                  | 1,010              |
| 32    | 81                  | 1,091              |
| 33    | 87                  | 1,178              |
| 34    | 93                  | 1,271              |
| 35    | 99                  | **1,370**          |
</details>

<details>
<summary><strong>4. Drakkar Shipyard</strong> — Base: 12 | Growth: 1.12 | Max: 20 | Total: 855 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 12                  | 12                 |
| 2     | 13                  | 25                 |
| 3     | 15                  | 40                 |
| 4     | 16                  | 56                 |
| 5     | 18                  | 74                 |
| 6     | 21                  | 95                 |
| 7     | 23                  | 118                |
| 8     | 26                  | 144                |
| 9     | 29                  | 173                |
| 10    | 33                  | 206                |
| 11    | 37                  | 243                |
| 12    | 41                  | 284                |
| 13    | 46                  | 330                |
| 14    | 52                  | 382                |
| 15    | 58                  | 440                |
| 16    | 65                  | 505                |
| 17    | 73                  | 578                |
| 18    | 82                  | 660                |
| 19    | 92                  | 752                |
| 20    | 103                 | **855**            |
</details>

<details>
<summary><strong>5. Explorer's House</strong> — Base: 12 | Growth: 1.20 | Max: 10 | Total: 305 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 12                  | 12                 |
| 2     | 14                  | 26                 |
| 3     | 17                  | 43                 |
| 4     | 20                  | 63                 |
| 5     | 24                  | 87                 |
| 6     | 29                  | 116                |
| 7     | 35                  | 151                |
| 8     | 42                  | 193                |
| 9     | 51                  | 244                |
| 10    | 61                  | **305**            |
</details>

<details>
<summary><strong>6. Eldrunar's Cave</strong> — Base: 8 | Growth: 1.06 | Max: 40 | Total: 1,221 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 8                   | 8                  |
| 2     | 8                   | 16                 |
| 3     | 8                   | 24                 |
| 4     | 9                   | 33                 |
| 5     | 10                  | 43                 |
| 6     | 10                  | 53                 |
| 7     | 11                  | 64                 |
| 8     | 12                  | 76                 |
| 9     | 12                  | 88                 |
| 10    | 13                  | 101                |
| 11    | 14                  | 115                |
| 12    | 15                  | 130                |
| 13    | 16                  | 146                |
| 14    | 17                  | 163                |
| 15    | 18                  | 181                |
| 16    | 19                  | 200                |
| 17    | 20                  | 220                |
| 18    | 21                  | 241                |
| 19    | 22                  | 263                |
| 20    | 24                  | 287                |
| 21    | 25                  | 312                |
| 22    | 27                  | 339                |
| 23    | 28                  | 367                |
| 24    | 30                  | 397                |
| 25    | 32                  | 429                |
| 26    | 34                  | 463                |
| 27    | 36                  | 499                |
| 28    | 38                  | 537                |
| 29    | 40                  | 577                |
| 30    | 43                  | 620                |
| 31    | 45                  | 665                |
| 32    | 48                  | 713                |
| 33    | 51                  | 764                |
| 34    | 54                  | 818                |
| 35    | 58                  | 876                |
| 36    | 61                  | 937                |
| 37    | 65                  | 1,002              |
| 38    | 69                  | 1,071              |
| 39    | 73                  | 1,144              |
| 40    | 77                  | **1,221**          |
</details>

<details>
<summary><strong>7. Runic Sanctuary</strong> — Base: 10 | Growth: 1.08 | Max: 30 | Total: 1,118 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 10                  | 10                 |
| 2     | 10                  | 20                 |
| 3     | 11                  | 31                 |
| 4     | 12                  | 43                 |
| 5     | 13                  | 56                 |
| 6     | 14                  | 70                 |
| 7     | 15                  | 85                 |
| 8     | 17                  | 102                |
| 9     | 18                  | 120                |
| 10    | 19                  | 139                |
| 11    | 21                  | 160                |
| 12    | 23                  | 183                |
| 13    | 25                  | 208                |
| 14    | 27                  | 235                |
| 15    | 29                  | 264                |
| 16    | 31                  | 295                |
| 17    | 34                  | 329                |
| 18    | 37                  | 366                |
| 19    | 39                  | 405                |
| 20    | 43                  | 448                |
| 21    | 46                  | 494                |
| 22    | 50                  | 544                |
| 23    | 54                  | 598                |
| 24    | 58                  | 656                |
| 25    | 63                  | 719                |
| 26    | 68                  | 787                |
| 27    | 73                  | 860                |
| 28    | 79                  | 939                |
| 29    | 86                  | 1,025              |
| 30    | 93                  | **1,118**          |
</details>

<details>
<summary><strong>8. Storage & Market</strong> — Base: 10 | Growth: 1.12 | Max: 20 | Total: 710 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 10                  | 10                 |
| 2     | 11                  | 21                 |
| 3     | 12                  | 33                 |
| 4     | 14                  | 47                 |
| 5     | 15                  | 62                 |
| 6     | 17                  | 79                 |
| 7     | 19                  | 98                 |
| 8     | 22                  | 120                |
| 9     | 24                  | 144                |
| 10    | 27                  | 171                |
| 11    | 31                  | 202                |
| 12    | 34                  | 236                |
| 13    | 38                  | 274                |
| 14    | 43                  | 317                |
| 15    | 48                  | 365                |
| 16    | 54                  | 419                |
| 17    | 61                  | 480                |
| 18    | 68                  | 548                |
| 19    | 76                  | 624                |
| 20    | 86                  | **710**            |
</details>

<details>
<summary><strong>9. Sacred Harvest Field</strong> — Base: 7 | Growth: 1.06 | Max: 40 | Total: 1,061 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 7                   | 7                  |
| 2     | 7                   | 14                 |
| 3     | 7                   | 21                 |
| 4     | 8                   | 29                 |
| 5     | 8                   | 37                 |
| 6     | 9                   | 46                 |
| 7     | 9                   | 55                 |
| 8     | 10                  | 65                 |
| 9     | 11                  | 76                 |
| 10    | 11                  | 87                 |
| 11    | 12                  | 99                 |
| 12    | 13                  | 112                |
| 13    | 14                  | 126                |
| 14    | 14                  | 140                |
| 15    | 15                  | 155                |
| 16    | 16                  | 171                |
| 17    | 17                  | 188                |
| 18    | 18                  | 206                |
| 19    | 19                  | 225                |
| 20    | 21                  | 246                |
| 21    | 22                  | 268                |
| 22    | 23                  | 291                |
| 23    | 25                  | 316                |
| 24    | 26                  | 342                |
| 25    | 28                  | 370                |
| 26    | 30                  | 400                |
| 27    | 31                  | 431                |
| 28    | 33                  | 464                |
| 29    | 35                  | 499                |
| 30    | 37                  | 536                |
| 31    | 40                  | 576                |
| 32    | 42                  | 618                |
| 33    | 45                  | 663                |
| 34    | 47                  | 710                |
| 35    | 50                  | 760                |
| 36    | 53                  | 813                |
| 37    | 57                  | 870                |
| 38    | 60                  | 930                |
| 39    | 64                  | 994                |
| 40    | 67                  | **1,061**          |
</details>

<details>
<summary><strong>10. Watch Tower</strong> — Base: 11 | Growth: 1.12 | Max: 20 | Total: 783 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 11                  | 11                 |
| 2     | 12                  | 23                 |
| 3     | 13                  | 36                 |
| 4     | 15                  | 51                 |
| 5     | 17                  | 68                 |
| 6     | 19                  | 87                 |
| 7     | 21                  | 108                |
| 8     | 24                  | 132                |
| 9     | 27                  | 159                |
| 10    | 30                  | 189                |
| 11    | 34                  | 223                |
| 12    | 38                  | 261                |
| 13    | 42                  | 303                |
| 14    | 47                  | 350                |
| 15    | 53                  | 403                |
| 16    | 60                  | 463                |
| 17    | 67                  | 530                |
| 18    | 75                  | 605                |
| 19    | 84                  | 689                |
| 20    | 94                  | **783**            |
</details>

<details>
<summary><strong>11. Víðarr's Woodland</strong> — Base: 6 | Growth: 1.06 | Max: 40 | Total: 911 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 6                   | 6                  |
| 2     | 6                   | 12                 |
| 3     | 6                   | 18                 |
| 4     | 7                   | 25                 |
| 5     | 7                   | 32                 |
| 6     | 8                   | 40                 |
| 7     | 8                   | 48                 |
| 8     | 9                   | 57                 |
| 9     | 9                   | 66                 |
| 10    | 10                  | 76                 |
| 11    | 10                  | 86                 |
| 12    | 11                  | 97                 |
| 13    | 12                  | 109                |
| 14    | 12                  | 121                |
| 15    | 13                  | 134                |
| 16    | 14                  | 148                |
| 17    | 15                  | 163                |
| 18    | 16                  | 179                |
| 19    | 17                  | 196                |
| 20    | 18                  | 214                |
| 21    | 19                  | 233                |
| 22    | 20                  | 253                |
| 23    | 21                  | 274                |
| 24    | 22                  | 296                |
| 25    | 24                  | 320                |
| 26    | 25                  | 345                |
| 27    | 27                  | 372                |
| 28    | 28                  | 400                |
| 29    | 30                  | 430                |
| 30    | 32                  | 462                |
| 31    | 34                  | 496                |
| 32    | 36                  | 532                |
| 33    | 38                  | 570                |
| 34    | 41                  | 611                |
| 35    | 43                  | 654                |
| 36    | 46                  | 700                |
| 37    | 48                  | 748                |
| 38    | 51                  | 799                |
| 39    | 54                  | 853                |
| 40    | 58                  | **911**            |
</details>

<details>
<summary><strong>12. Dvergr's Quarry</strong> — Base: 6 | Growth: 1.06 | Max: 40 | Total: 911 pts</summary>

| Level | Points (this level) | Total (cumulative) |
|:-----:|:-------------------:|:------------------:|
| 1     | 6                   | 6                  |
| 2     | 6                   | 12                 |
| 3     | 6                   | 18                 |
| 4     | 7                   | 25                 |
| 5     | 7                   | 32                 |
| 6     | 8                   | 40                 |
| 7     | 8                   | 48                 |
| 8     | 9                   | 57                 |
| 9     | 9                   | 66                 |
| 10    | 10                  | 76                 |
| 11    | 10                  | 86                 |
| 12    | 11                  | 97                 |
| 13    | 12                  | 109                |
| 14    | 12                  | 121                |
| 15    | 13                  | 134                |
| 16    | 14                  | 148                |
| 17    | 15                  | 163                |
| 18    | 16                  | 179                |
| 19    | 17                  | 196                |
| 20    | 18                  | 214                |
| 21    | 19                  | 233                |
| 22    | 20                  | 253                |
| 23    | 21                  | 274                |
| 24    | 22                  | 296                |
| 25    | 24                  | 320                |
| 26    | 25                  | 345                |
| 27    | 27                  | 372                |
| 28    | 28                  | 400                |
| 29    | 30                  | 430                |
| 30    | 32                  | 462                |
| 31    | 34                  | 496                |
| 32    | 36                  | 532                |
| 33    | 38                  | 570                |
| 34    | 41                  | 611                |
| 35    | 43                  | 654                |
| 36    | 46                  | 700                |
| 37    | 48                  | 748                |
| 38    | 51                  | 799                |
| 39    | 54                  | 853                |
| 40    | 58                  | **911**            |
</details>

---

#### Point Distribution Summary

| Building                 | Max Points | % of Total |
|--------------------------|:----------:|:----------:|
| Berserker's Barrack      | 1,370      | 12.1%      |
| Eldrunar's Cave          | 1,221      | 10.8%      |
| Jarl's Hall              | 1,217      | 10.8%      |
| Runic Sanctuary          | 1,118      | 9.9%       |
| Sacred Harvest Field     | 1,061      | 9.4%       |
| Víðarr's Woodland        | 911        | 8.1%       |
| Dvergr's Quarry          | 911        | 8.1%       |
| Freyja's Temple          | 855        | 7.6%       |
| Drakkar Shipyard         | 855        | 7.6%       |
| Watch Tower              | 783        | 6.9%       |
| Storage & Market         | 710        | 6.3%       |
| Explorer's House         | 305        | 2.7%       |
| **TOTAL**                | **11,317** | **100%**   |

> **Ranking:** Village ranking is determined by total village points. Players with multiple villages sum all village points for their **global ranking**. Alliance ranking sums all member village points.

> **TODO:** Define additional scoring categories — military points (troops trained/killed), exploration points, and seasonal bonus multipliers.

---

### 8.16 Conquest

#### Aggressive Conquest
> **TODO:** Define military conquest mechanics.

#### Peaceful Conquest
> **TODO:** Define cultural/diplomatic conquest mechanics.

---

### 8.17 Alliance

> **TODO:** Define alliance system — creation, roles, diplomacy, shared objectives.

---

### 8.18 Messages

> **TODO:** Define messaging system — player-to-player, alliance chat, battle reports.

---

### 8.19 Events & Seasons

> **TODO:** Define world events (e.g., Ragnarök cycles), seasonal content, and time-limited challenges.

---

### 8.20 NPCs

> **TODO:** Define NPC behavior — barbarian villages, mythological creatures, world bosses.

---

### 8.21 Exploration

> **TODO:** Define exploration mechanics — realm travel, fog of war, discovery rewards.

---

## 9. Architecture

| Component            | Technology / Tool | Notes                            |
|----------------------|-------------------|----------------------------------|
| **Engine / Editor**  | Custom (C++)      | Client-side rendering & UI       |
| **Scripting Language** | C++             | Game logic on client              |
| **Backend Language** | Golang            | Server-side game logic            |
| **Database**         | PostgreSQL        | Persistent game state             |
| **CDN**              | TBD               | Static asset delivery             |
| **Identity**         | TBD               | Authentication & authorization    |
| **Communication**    | TBD               | Real-time (WebSocket / gRPC)      |
| **Scalability**      | TBD               | Horizontal scaling strategy       |

---

## 10. Roadmap

### Phase 1 — MVP
> **TODO:** Define MVP milestones and timeline.

### Phase 2 — Alpha v1.0.0
> **TODO:** Define Alpha feature set.

### Phase 3 — v2.0.0
> **TODO:** Define v2.0 feature set.

---

## 11. Appendix — Brainstorm & Research Notes

> ⚠️ **Note:** The content below is raw brainstorm material and research notes. It is **not part of the formal GDD** but serves as reference for future design decisions.

---

### A. Innovative Mechanics (Applicable to Various Themes)

<details>
<summary><strong>Legacy / Dynasty System</strong></summary>

Your main leader ages and eventually dies. You must train heirs, each with different traits and abilities, ensuring continuity and evolution. Decisions made by one leader can have consequences for future generations.
</details>

<details>
<summary><strong>Dynamic Trade Routes</strong></summary>

Instead of producing everything internally, create strong interdependence between players through trade. Resources could be scarcer in certain regions, encouraging trade routes that can be attacked, taxed, or protected.
</details>

<details>
<summary><strong>Environmental Impact & Natural Disasters</strong></summary>

Excessive resource exploitation or certain magical/industrial actions could lead to environmental consequences (pollution, resource depletion, elemental monster summons). Random natural disasters (earthquakes, volcanic eruptions, cosmic storms) could reshape the map or destroy buildings.
</details>

<details>
<summary><strong>Active & Customizable Unit Abilities</strong></summary>

Some units could have active abilities the player controls during combat (simplified — choosing tactics before battle) or customizable with "equipment" or "runes".
</details>

<details>
<summary><strong>City Morale & Culture System</strong></summary>

Population happiness and culture could affect production, troop loyalty, and research speed. A strong culture could "convert" nearby villages peacefully or resist enemy attacks better.
</details>

<details>
<summary><strong>Light RPG Elements for Leaders/Heroes</strong></summary>

Leaders or special heroes gain experience, level up, and unlock abilities that benefit the empire or troops in combat. They could be captured, rescued, or even betray.
</details>

<details>
<summary><strong>Collaborative World Events</strong></summary>

Powerful NPC invasions, world bosses, or the need to build a wonder together (or compete to build it first) — requiring cooperation (or intense competition) across multiple alliances.
</details>

---

### B. Deity System — Design Ideas

<details>
<summary><strong>Core Concept</strong></summary>

- Deities provide bonuses to cities (similar to Grépolis), plus special units and divine powers.
- If the player **violates certain principles**, the deity applies **punishments** (earthquakes, demanded sacrifices, etc.).
- If a deity loses enough **Respect**, it can **betray and abandon** the city with a final devastating punishment.
</details>

---

### C. Deity Leader Options — Deep Dive

<details>
<summary><strong>Option 1: Freya (Vanir Goddess — Love, Beauty, Fertility, War & Death)</strong></summary>

**Role:** Versatile, powerful leader.

| Focus Area         | Bonus                                                                 |
|--------------------|-----------------------------------------------------------------------|
| **Population**     | Accelerated growth and food production                                 |
| **Military**       | Elite cavalry and warrior units (inspired by Valkyries)                |
| **Divine Powers**  |                                                                       |
| *Sessrúmnir's Call*  | Accelerate training or revive a % of lost troops after battle          |
| *Folkvang's Blessing* | Boost specific resource production or improve troop morale            |
| *Seidr Magic*       | Enhanced espionage or short-duration curse on enemy city (↓ production, ↓ defense) |
</details>

<details>
<summary><strong>Option 2: Skadi (Giantess & Goddess — Hunt, Winter, Mountains)</strong></summary>

**Role:** Survival, terrain tactics, and ambush specialist.

| Focus Area         | Bonus                                                                 |
|--------------------|-----------------------------------------------------------------------|
| **Terrain**        | Advantages in forests, mountains, and winter events                    |
| **Military**       | Elite scouts and archers (stealth, bonus damage vs. specific units)    |
| **Divine Powers**  |                                                                       |
| *Winter Fury*       | Temporary blizzard on enemy province (↓ troop speed, ↓ production)     |
| *Hunter's Instinct* | Reveal hidden enemy units or enhanced espionage report                 |
| *Mountain's Blessing* | Increase city wall defense or stone/metal production                  |
</details>

<details>
<summary><strong>Option 3: Tyr (God of War, Justice & Oaths)</strong></summary>

**Role:** Military honor, discipline, and infantry power.

| Focus Area         | Bonus                                                                 |
|--------------------|-----------------------------------------------------------------------|
| **Infantry**       | Increased attack, defense, or morale for front-line troops             |
| **Training**       | Reduced cost or time for military unit training                        |
| **Divine Powers**  |                                                                       |
| *Battle Oath*       | Significant attack boost for short duration (small defense penalty after) |
| *Fair Judgment*     | Reveal true enemy army strength (counter bluffs/false espionage)       |
| *Tyr's Hand*        | Unique buff making one unit exceptionally powerful for a single battle  |
</details>

---

### D. Core Design Principles (Reminders)

- **Keep the Core Loop Addictive:** Build → Collect → Train → Expand → Fight must feel satisfying and rewarding.
- **Focus on Social Interaction:** Alliances, wars, trade, and diplomacy are the heart of the game.
- **Balancing is Essential:** For a fair and fun long-term experience.
- **Clear Progression:** Players must feel constant advancement toward new goals.
- **Ethical Monetization (if applicable):** Avoid excessive pay-to-win models.

---

### E. Other Notes

- Consider introducing the **Nine Worlds** and **Yggdrasil** as the core map structure — perhaps the map itself represents transitions between the 9 realms.
- Explore a hero roster of **exactly 9 heroes** — one per realm. This creates a strong thematic tie but presents significant balancing challenges.
