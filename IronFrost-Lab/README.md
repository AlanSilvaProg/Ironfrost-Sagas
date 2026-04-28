# IronFrost-Lab

Projeto Unity utilizado como **laboratório de filmagem e composição 3D** para gerar os assets 2D (sprites e sprite sheets) usados no jogo **Ironfrost Sagas**.

---

## 🎯 Propósito

Este projeto **não é o jogo em si**. Ele é um ambiente controlado onde modelos 3D são posicionados, animados e filmados com fundo verde para, ao final de um pipeline de pós-produção, gerar sprite sheets prontos para uso no engine do jogo.

---

## 🔄 Pipeline de Geração de Assets

O processo completo segue as etapas abaixo, da geração do modelo 3D até o sprite sheet final:

```
[Tripo3D] → [Unity (IronFrost-Lab)] → [Filmagem] → [GIF] → [Frames] → [Remoção de Fundo] → [Sprite Sheet]
```

### 1. 🧊 Geração do Modelo 3D — Tripo3D

- Os modelos 3D são gerados na plataforma **[Tripo3D](https://www.tripo3d.ai/)**.
- Após a geração, os modelos são exportados e importados na pasta `Assets/TripoModels/` deste projeto Unity.

---

### 2. 🗺️ Composição na Cena — Unity

- O modelo 3D é posicionado **sobre o PNG do terreno** (`CityExpanded.png` ou variantes `city*.png`), servindo como referência visual para escala, perspectiva e proporção.
- A **rotação do modelo é ajustada** para corresponder ao **ângulo da câmera isométrica** utilizada no jogo, garantindo coerência visual com o ambiente.
- Se necessário, são adicionados:
  - Animações (ex: idle, ataque, movimentação)
  - Novos elementos complementares direto pelo Unity (partículas, acessórios, etc.)

---

### 3. 🟩 Filmagem com Fundo Verde

- A cena é configurada com um **background verde sólido** (chroma key).
- A câmera é posicionada no ângulo isométrico definitivo do jogo.
- O modelo/animação é gravado diretamente pelo Unity (Game View recording ou ferramenta equivalente).

---

### 4. 🎞️ Conversão para GIF

- O vídeo gravado é convertido em um **GIF de aproximadamente 8 frames**.
- O GIF deve cobrir um ciclo completo da animação (ex: um loop de idle ou um ciclo de ataque).
- Ferramentas sugeridas: **FFmpeg**, **EZGif**, ou similar.

```bash
# Exemplo com FFmpeg
ffmpeg -i input.mp4 -vf "fps=8,scale=256:-1" -loop 0 output.gif
```

---

### 5. 🖼️ Extração de Frames

- O GIF é desmontado em **1 imagem PNG por frame**.
- Cada frame deve ser salvo com nomenclatura sequencial (ex: `asset_frame_01.png`, `asset_frame_02.png`…).
- Ferramentas sugeridas: **FFmpeg**, **ImageMagick**, ou **EZGif**.

```bash
# Exemplo com ImageMagick
convert output.gif frame_%02d.png
```

---

### 6. 🧹 Remoção do Fundo Verde

- O fundo verde é removido de **cada frame individualmente**, deixando o fundo transparente (canal alpha).
- O resultado é um PNG com transparência por frame.
- Ferramentas sugeridas: **GIMP** (script-fu batch), **Photoshop** (ação em lote), **rembg** (Python), ou **EZGif**.

```bash
# Exemplo com rembg (Python)
for f in frame_*.png; do rembg i "$f" "nobg_$f"; done
```

---

### 7. 📦 Empacotamento em Sprite Sheet

- Todos os frames sem fundo são compactados em um único **sprite sheet**.
- O sprite sheet deve seguir o padrão de nomenclatura e dimensões do projeto Ironfrost Sagas.
- Ferramentas sugeridas: **TexturePacker**, **Shoebox**, **Free Texture Packer**, ou **gdx-texture-packer**.

---

## ✅ Asset Finalizado

Ao final do pipeline, o asset está **pronto para ser importado** no engine do jogo como um sprite sheet animado.

```
Assets/
└── city*.png              ← Terreno de referência para composição
└── TripoModels/           ← Modelos 3D importados do Tripo3D
└── Models/                ← Modelos organizados por tipo
└── Prefabs/               ← Prefabs configurados para filmagem
└── Scenes/                ← Cenas de filmagem por asset
```

---

## 🛠️ Ferramentas Utilizadas

| Etapa                | Ferramenta              |
|----------------------|-------------------------|
| Geração 3D           | Tripo3D                 |
| Composição e Filmagem| Unity (IronFrost-Lab)   |
| Conversão para GIF   | FFmpeg / EZGif          |
| Extração de Frames   | FFmpeg / ImageMagick    |
| Remoção de Fundo     | rembg / GIMP / Photoshop|
| Sprite Sheet         | TexturePacker / Shoebox |

---

> **Nota:** Este projeto é exclusivo para geração de assets e **não deve ser confundido com o engine ou backend do jogo**. O projeto principal do jogo está em `GameProject-(Ex-Engine)/`.
