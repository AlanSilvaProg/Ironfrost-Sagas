# 🏗️ Checklist — Adding a New Building

> This document describes everything that must be updated in the GDD when adding a **new building** to Ironfrost Sagas.

---

## Pre-Design Requirements

Before adding a new building, define the following:

- [ ] **Name** — Must be thematically tied to Norse mythology
- [ ] **Function** — What is the building's primary role?
- [ ] **Max Level** — How many levels does it have? (affects cost growth and point growth)
- [ ] **Lore Description** — Short mythological flavor text

---

## GDD Sections to Update

### 1. Building Overview Table (Section 8.7)

**File:** `GDD - Ironfrost Sagas ( Temp name ).md`
**Section:** `8.7 Buildings → Building Overview`

Add a new row to the Building Overview table with:

| Field | Description |
|-------|-------------|
| `#` | Sequential building number |
| `Building` | Name (bold) |
| `Function` | Short description |
| `Max Level` | Maximum upgrade level |
| `🪵 Wood` | Base cost (level 1) in Wood |
| `🪨 Rock` | Base cost (level 1) in Rock |
| `⛏️ Metal` | Base cost (level 1) in Metal |
| `✨ Eldrunar` | Base cost (level 1) in Eldrunar, or `—` if none |
| `Cost Growth` | Percentage increase per level |

**Guidelines for cost growth:**
- **Max 10 levels** → Growth: 28–30%
- **Max 20 levels** → Growth: 22–28%
- **Max 30 levels** → Growth: 18–22%
- **Max 35 levels** → Growth: 15–18%
- **Max 40 levels** → Growth: 13–16%

---

### 2. Cost Progression Table (Section 8.7)

**Section:** `8.7 Buildings → Cost Progression Tables`

Add a new `<details>` collapsible block with the building's cost at key levels.

**Formula:**
```
CostAtLevel(L) = floor( BaseCost × CostGrowth ^ (L - 1) )
```

Show at minimum: levels 1–5, then every 5 levels up to max.

---

### 3. Total Investment Cost Summary (Section 8.7)

**Section:** `8.7 Buildings → Total Investment Cost Summary`

- Add the new building's **total cost across all levels** to the summary table.
- **Recalculate** the "Total investment to max a village" value at the bottom.

---

### 4. Points & Ranking — Building Parameters (Section 8.15)

**Section:** `8.15 Points & Ranking → Building Parameters`

Add a new row with:

| Field | Description |
|-------|-------------|
| `Base Points` | Starting points at level 1 |
| `Growth Rate` | Multiplier per level (e.g., 1.06, 1.08, 1.12) |
| `Max Level` | Same as defined above |
| `Max Points` | Sum of all level points |

**Guidelines for point base/growth:**
- Resource buildings (max 40) → Base: 5–8, Growth: 1.06
- Military buildings (max 30-35) → Base: 8–10, Growth: 1.07–1.08
- Core buildings (max 20) → Base: 10–17, Growth: 1.12
- Utility buildings (max 10) → Base: 10–12, Growth: 1.20

**Formula:**
```
PointsGainedAtLevel(L) = floor( BasePoints × GrowthRate ^ (L - 1) )
```

---

### 5. Point Progression Table (Section 8.15)

**Section:** `8.15 Points & Ranking → Point Progression Tables`

Add a new `<details>` collapsible block showing points per level (all levels, 1 to max).

---

### 6. Point Distribution Summary (Section 8.15)

**Section:** `8.15 Points & Ranking → Point Distribution Summary`

- Add the new building to the distribution table
- **Recalculate ALL percentages** (they must sum to 100%)
- **Update the TOTAL** max village score
- **Update the "Maximum Village Score"** callout

---

### 7. Resource Collection (Section 8.8) — If Applicable

If the building provides a **resource collection multiplier**, update:

- **Base Collection Rate table** → Update the "Source Building" and "Building Bonus Formula" columns
- **Building Multiplier Progression** → Add a new `<details>` block showing the multiplier at every 5 levels
- **Max Production Scenario** → Recalculate with the new building's effect

---

### 8. Storage Capacity (Section 8.7) — If Applicable

If the building affects **storage capacity**, add a storage capacity progression table inside the building's cost progression `<details>` block.

---

### 9. Table of Contents (Top of Document)

If the building introduces a **new subsection** in the GDD (rare), update the Table of Contents at the top of the document.

---

## Validation Checklist

After all updates, verify:

- [ ] Building appears in the **Overview Table** (Section 8.7)
- [ ] **Cost progression table** exists with correct values
- [ ] Building is in the **Total Investment Summary** with correct total
- [ ] **"Total investment to max a village"** is recalculated
- [ ] Building appears in **Building Parameters** scoring table (Section 8.15)
- [ ] **Point progression table** exists with all levels
- [ ] Building is in the **Point Distribution Summary** with correct percentage
- [ ] **Maximum Village Score** is updated everywhere it appears
- [ ] Resource collection tables updated (if applicable)
- [ ] Storage capacity tables updated (if applicable)
- [ ] All numbers are computed using the documented formulas (no manual guesses)

---

## Quick Reference — Formulas

| What | Formula |
|------|---------|
| **Upgrade Cost** | `floor(BaseCost × CostGrowth^(L-1))` |
| **Points per Level** | `floor(BasePoints × PointGrowth^(L-1))` |
| **Storage Capacity** | `floor(BaseCap × StorageGrowth^(L-1))` |
| **Resource Multiplier** | `1 + (BuildingLevel × BonusFactor)` |
| **Population Gain** | `floor(5 × 1.08^(L-1))` |
