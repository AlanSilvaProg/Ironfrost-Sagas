# ⚔️ Checklist — Adding a New Troop

> This document describes everything that must be updated in the GDD when adding a **new troop** to Ironfrost Sagas.

---

## Pre-Design Requirements

Before adding a new troop, define the following:

- [ ] **Name** — Must be thematically tied to Norse mythology
- [ ] **Type** — Infantry, Cavalry, Ranged, Siege, Naval, Special/Mythological
- [ ] **Tier** — Basic, Advanced, Elite, Mythological
- [ ] **Training Building** — Which building trains this troop?
- [ ] **Unlock Requirements** — Building level, research, deity, etc.
- [ ] **Lore Description** — Short mythological flavor text

---

## Troop Stats to Define

Every troop needs the following attributes:

### Combat Stats

| Stat | Description | Example Range |
|------|-------------|---------------|
| **Attack** | Base offensive power | 5–200 |
| **Defense (Melee)** | Resistance vs melee attacks | 5–150 |
| **Defense (Ranged)** | Resistance vs ranged attacks | 5–150 |
| **HP** | Hit points | 20–500 |
| **Speed** | Movement speed on world map (tiles/hour) | 5–40 |
| **Carry Capacity** | Resources the unit can loot | 10–200 |

### Training Cost

| Resource | Description |
|----------|-------------|
| 🪵 **Wood** | Amount of Wood required |
| 🪨 **Rock** | Amount of Rock required |
| ⛏️ **Metal** | Amount of Metal required |
| ✨ **Eldrunar** | Amount needed (special troops only) |
| 🥩 **Meat** | Food consumed during training |
| 🦌 **Animal Skin** | Skin needed for armor/gear |
| 👥 **Population** | Number of citizens consumed |

### Training Requirements

| Attribute | Description |
|-----------|-------------|
| **Training Time** | Base time to train 1 unit (seconds) |
| **Training Building** | Which building trains this unit |
| **Min Building Level** | Minimum level of the training building |
| **Research Required** | Any Runic Sanctuary research needed? |
| **Deity Required** | Is this troop locked behind a specific deity? |

---

## GDD Sections to Update

### 1. Troops Section (Section 8.9)

**File:** `GDD - Ironfrost Sagas ( Temp name ).md`
**Section:** `8.9 Troops`

Add the troop to the **troop roster table** with all combat stats, costs, and requirements.

Structure:
```
| Name | Type | ATK | DEF (M) | DEF (R) | HP | Speed | Carry | Pop | Training Time |
```

---

### 2. Training Costs Table (Section 8.9)

Add a row to the **training cost table** showing resource costs per unit:

```
| Name | Wood | Rock | Metal | Eldrunar | Meat | Animal Skin | Pop |
```

---

### 3. Training Building (Section 8.7)

**Section:** `8.7 Buildings`

Update the relevant **training building's description** to include the new troop in its "Responsible for" field.

For example, if adding a troop to Berserker's Barrack:
- Update the Building Overview table row
- Update the building's detailed description (if any)

---

### 4. Special Troops (Section 8.11) — If Mythological

If the troop is a **special/mythological** unit:
- Add it to the **Special Troops** section
- Document the **deity** or **Eldrunar** requirement
- Define any **special abilities** the unit has

---

### 5. Ships (Section 8.12) — If Naval

If the troop is a **naval unit/ship**:
- Add it to the **Ships** section
- Document the **Drakkar Shipyard** level required
- Define **naval-specific stats** (cargo capacity, boarding power, etc.)

---

### 6. Heroes (Section 8.10) — If Hero

If the troop is a **hero unit**:
- Add it to the **Heroes** section
- Define the hero's **unique abilities**
- Define **experience/leveling** progression
- Define which **realm/area** the hero is tied to

---

### 7. Battles Section (Section 8.14)

Consider how the new troop affects the **battle system**:

- Does it have a **counter** (strong against specific troop types)?
- Does it have a **weakness** (vulnerable against specific troop types)?
- Does it require special **battle tactics** or **formations**?
- Does it change the **attack/defense calculation** in any way?

Update the battle system documentation if needed.

---

### 8. Buff & Nerf Elements (Section 8.13)

If the troop interacts with:
- **Deity buffs** — Document which deities enhance this troop
- **Runic research buffs** — Document which runes affect this troop
- **Environmental effects** — Does terrain affect this troop differently?

---

### 9. Points & Ranking (Section 8.15) — If Military Points Exist

If a **military points** system has been defined:
- Define how many military points this troop contributes
- Update the military scoring calculations

---

### 10. Tutorial (Section 8.2)

Consider if the tutorial needs updating:
- Is this an **early-game troop** that new players should train?
- Does it affect the tutorial flow?

---

### 11. Balance Verification

After adding the troop, verify balance:

- [ ] **Cost-to-power ratio** is fair compared to similar-tier troops
- [ ] **Training time** scales appropriately with power level
- [ ] **Counter system** — every troop should have a counter
- [ ] **Population cost** — stronger troops should cost more citizens
- [ ] **Resource diversity** — not all troops should require the same resources
- [ ] **Building level gate** — appropriate level requirement

---

## Validation Checklist

After all updates, verify:

- [ ] Troop appears in the **Troops roster** (Section 8.9)
- [ ] **Training costs** are defined for all resources
- [ ] **Combat stats** are complete (ATK, DEF, HP, Speed, Carry)
- [ ] **Training requirements** are defined (building, level, research)
- [ ] **Training building** description updated to mention the new troop
- [ ] **Special abilities** documented (if any)
- [ ] **Deity/research requirements** documented (if any)
- [ ] **Battle interactions** considered (counters, weaknesses)
- [ ] **Balance** verified against existing troops of the same tier
- [ ] No single troop dominates without a viable counter

---

## Troop Design Template

Copy and fill this template when designing a new troop:

```markdown
### [Troop Name]

**Type:** [Infantry / Cavalry / Ranged / Siege / Naval / Special]
**Tier:** [Basic / Advanced / Elite / Mythological]
**Lore:** [1-2 sentence backstory]

#### Combat Stats
| ATK | DEF (M) | DEF (R) | HP | Speed | Carry |
|:---:|:-------:|:-------:|:--:|:-----:|:-----:|
| ??? | ???     | ???     | ???| ???   | ???   |

#### Training Cost
| Wood | Rock | Metal | Eldrunar | Meat | Skin | Pop |
|:----:|:----:|:-----:|:--------:|:----:|:----:|:---:|
| ???  | ???  | ???   | ???      | ???  | ???  | ??? |

#### Requirements
- **Building:** [Name] Level [X]
- **Research:** [Name] (if any)
- **Deity:** [Name] (if any)

#### Strong Against: [Troop types]
#### Weak Against: [Troop types]
#### Special Abilities: [Description]
```

---

## Quick Reference — Troop Tiers

| Tier | Power Level | Pop Cost | Typical Requirements |
|------|-------------|----------|----------------------|
| **Basic** | Low | 1 | Barrack Level 1-5 |
| **Advanced** | Medium | 2-3 | Barrack Level 10-20 + Research |
| **Elite** | High | 4-6 | Barrack Level 25-35 + Advanced Research |
| **Mythological** | Very High | 8-15 | Temple + Deity + Eldrunar |
