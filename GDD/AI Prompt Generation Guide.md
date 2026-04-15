# AI Prompt Generation Guide

Este documento serve como a "bíblia visual" para a geração de assets artísticos do **Ironfrost Sagas** utilizando Inteligência Artificial Generativa (Midjourney, DALL-E, Stable Diffusion, etc.). O objetivo é garantir que todas as artes do jogo compartilhem um estilo consistente e altamente profissional, evitando os vícios comuns da IA (como fotorealismo excessivo ou armaduras de fantasia genérica).

---

## 1. Guia de Geração para Tropas

As tropas do Ironfrost Sagas seguem um estilo **2D Cel-Shaded**. Elas devem parecer ativos de jogo vetoriais limpos, e não pinturas a óleo ou modelos 3D realistas.

### 📜 Regras de Ouro para Tropas:
1. **Imagem de Referência sempre:** Sempre anexe a imagem oficial e finalizada do *Berserker* (ou da sua tropa que ficou mais perfeita) e peça para a IA usar **puramente para referência de estilo de renderização**.
2. **Sem Cenário:** Prompts de tropas devem sempre pedir *Pure White Background* ou *Transparent Background*. O ambiente polui o asset.
3. **Perspectiva:** *Dramatic 3/4 front-facing portrait, framing from the waist up*. A arma primária deve estar em destaque.
4. **Acabamento e Contraste:** Roupas vikings devem ser em tons terrosos, escuros e frios. Cores muito vivas ou neon só devem ser usadas em elementos mágicos específicos (como runas brilhantes).

### ❌ Negative Prompt Padrão (Use em todas as tropas)
> photorealistic, 3D render, CGI, cute, anime, superhero, glossy, shiny knight plate armor, medieval European crusader, bikini armor, sexualized, huge magical sword, glowing magic aura covering body, background scenery, landscape, forest, UI text, numbers, arrows, watermark.

### 📝 Estrutura Padrão do Prompt de Tropa:
Para gerar uma tropa nova, basta seguir o template e preencher as Lacunas na descrição física.

> **Use the attached character illustration purely for the rendering style (flat cel-shading, bold outlines, clean 2D game asset quality).**
>
> Generate a **character portrait illustration** of an imposing Norse **[NOME O PAPEL DA TROPA]** to be used as a **unit card / recruitment screen artwork**.
>
> **Composition:**
> — Dramatic 3/4 front-facing portrait, framing from the waist up
> — Pure White Background — entirely isolated character, absolutely no environment
> — Camera at chest level. [DESCREVER POSTURA AQUI, ex: Aggressive and leaning forward]
>
> **Character Visuals:**
> — [DESCREVER O TIPO FÍSICO AQUI, ex: Scarred, massive Viking warlord]
> — **Armor:** [DESCREVER MATERIAIS DE ARMADURA. Foque em cota de malha, couro e pedaços de ferro]. 
> — **Weapons:** [DESCREVER ARMAS].
> 
> **Colors & Mood:**
> — **Color Palette:** [DESCREVER A PALETA DE NÚCLEOS PRINCIPAIS].
> — The illustration should convey: [SENTIMENTO PRINCIPAL. Ex: unbreakable defense, terror, speed].

---

## 2. Guia de Geração para Construções (Buildings)

Diferente das tropas de meio corpo, os prédios são vistos numa perspectiva isométrica ou frontal inclinada inteira. A arquitetura principal viking deve girar em torno de pesados troncos de madeira, telhados sobrepostos (estilo *Stave*) e reforços grosseiros de pedra e ferro no late-game.

### Construções em Evolução
Ao desenvolver modelos para a cidade, os prédios passam pelo período de atualização e construção. Abaixo estão os filtros em formato de prompt para gerar arte de "Obras" consistentes com as artes finais (Nível 20) lançadas anteriormente.

---

### 📝 Estrutura Padrão do Prompt de Construção (Prédio Novo / Arte Final):
Para gerar um prédio completamente inédito (idealmente focando logo no Nível 20/Fase Final para cravar o visual mais rico do prédio), utilize esta fórmula de preenchimento.

> **Use the attached reference image purely for the rendering style (flat cel-shading, bold outlines, clean 2D game asset quality, vector illustration style).**
>
> Generate a **building illustration** of a Norse **[NOME DA CONSTRUÇÃO]** to be used as a **building icon / city management asset** in a Viking-themed strategy game interface.
>
> **Composition:**
> — Isometric or 3/4 front-facing perspective, clearly displaying depth
> — Pure White Background — completely isolated architectural asset, absolutely no background scenery, sky, or landscape
> — Sitting on a small, clean circular patch of dirt, stone, and patchy snow
>
> **Architectural Design:**
> — **Base & Structure:** [DESCREVER FORMATO, PESO E MATERIAIS PRINCIPAIS. ex: A heavy log cabin structure with strict geometric lines, built using interlocking thick treated pine logs]
> — **Viking Detailing:** [DESCREVER ELEMENTOS NÓRDICOS E TELHADOS. ex: Steeply pitched dark wood shingle roofs overlapping each other, wooden shields nailed to the walls]
> — **Specific Features:** [DESCREVER O QUE FAZ O PRÉDIO SER ÚNICO. ex: An enormous glowing forge in the center / A large wooden water wheel on the side]
>
> **Colors & Mood:**
> — Lighting is clean and clearly highlights the texture of the materials (wood grain, stone cracks, iron rivets) without becoming muddy or photorealistic
> — **Color Palette:** [EX: Deep rich mahogany browns, cold stone greys, and the sharp orange of the forge fire]
> — The illustration should convey: [SENTIMENTO DO PRÉDIO. ex: industrial heavy production, unyielding defense, sacred divine atmosphere]

---

### 🎨 Edifício Nascendo do Zero (Fase 1 / Fundação)
**Como usar:** Use este prompt junto com a imagem do Nível 1 (ou final) da construção. O objetivo é criar a primeira fundação esquelética.

> **Use the attached building image perfectly as context for the target structure, and strictly match its exact rendering style (flat cel-shading, clear outlines, isometric 2D game asset).**
>
> Generate an **"under construction" building illustration** for a Viking strategy game, showing the bare foundations of a new building being erected from scratch.
>
> **Composition:**
> — Isometric or 3/4 perspective matching the source style
> — Pure White Background — isolated game asset
> — Placed on a patch of trampled dirt and snow
>
> **Construction Details:**
> — Only the very basic wooden skeleton/frame of the building has been raised so far. The structure is mostly hollow and transparent at this point, showing the floor plan
> — Crude wooden scaffolding and ladders lean against the partially raised beams
> — Scattered around the base are piles of raw construction materials: freshly cut tree logs, coiled heavy ropes, tools, and a wooden wheelbarrow
> — **No roof, no fire, no walls.** It looks like an active, busy medieval construction site on day one.
>
> **Colors & Mood:**
> — Warm, raw wood colors dominating. Fresh pine, dirt browns, and hemp rope colors. It should feel like a busy, optimistic start to a settlement.

---

### 🎨 Edifício em Reforma / Upgrade (Genérico para todas as fases)
**Como usar:** Mande pra IA a sua "Arte Completa" de um edifício e use esse prompt para forçar ela a "envelopar" aquele edifício exato com um visual de canteiro de obras.

> **CRITICAL: Use the attached building image heavily. Keep the exact core shape, silhouette, and rendering style (flat cel-shading, 2D isometric asset) of the attached building, but visually transform it into an active construction site.**
>
> Generate an **"Under Upgrade / Retrofitting" state** for the attached Viking building, used in a strategy game to show a building actively leveling up.
>
> **Transformations to apply to the original image:**
> — **Scaffolding:** Wrap the exterior of the building realistically in heavy wooden scaffolding (crude wooden poles, crossbeams, and elevated plank walkways meant for Viking builders)
> — **Tarps & Covers:** Partially cover the roof or one of the main walls with a heavy, weather-worn linen tarp/canvas (pale beige or grey) to hide parts of the upgrade process
> — **Construction Equipment:** Add a heavy wooden crane system or pulley with thick ropes hanging near the top. Place piles of raw materials (cut stone blocks, stacked timber, barrels) at the base of the building
> — Ensure the original identity of the building is still clearly recognizable underneath all the scaffolding and tarps
>
> **Composition:**
> — Pure White Background
> — Isometric/front-facing, exactly replicating the layout of the source image.
> — It must look like an active, heavy Viking upgrading process, preparing the building to emerge stronger.
