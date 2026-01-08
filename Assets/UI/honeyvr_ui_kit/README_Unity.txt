# HoneyVR UI Kit (Unity)

Questo pacchetto contiene sprite PNG pensati per UI di dialoghi/menù in VR (World Space Canvas).
Palette usata: D3AA5A, AB9431, B5B239, D5E0B5, C6DCDB, 9EC8D5, F1F3F1.

## Import in Unity
1) Trascina la cartella `honeyvr_ui_kit` dentro `Assets/Art/UI/`.
2) Seleziona tutti i PNG -> Inspector:
   - Texture Type: Sprite (2D and UI)
   - Sprite Mode: Single
   - Mesh Type: Full Rect
   - Filter Mode: Bilinear (o Point se vuoi pixel-crisp)
   - Compression: None (per evitare artefatti nei bordi)
   - Alpha Is Transparency: ON

## 9-slice (consigliato per pannelli e bottoni)
Per pannelli e bottoni puoi usare lo Sprite Editor -> Border:
- panel_dialog_large.png (1024x768): Border ~ 90 (L/R) e 90 (Top/Bottom)
- panel_dialog_medium.png (900x560): Border ~ 80
- panel_dialog_small.png (640x420): Border ~ 70
- button_* (512x160): Border ~ 60 (L/R) e 50 (T/B)
- input_field (760x150): Border ~ 55
- slider/progress track: Border ~ 24–30

Valori esatti: punta a “tagliare” appena dentro al bordo/scanalatura, lasciando intatte le curve.

## Uso rapido (suggerimenti)
- Dialogo NPC: `panel_dialog_*` + `speech_bubble` (a seconda dello stile)
- Bottoni: `button_primary_*` per CTA (Conferma/OK), `button_secondary_*` per Azioni secondarie
- Icon button: `button_hex_*` + `icon_*` sopra (Image)
- Modal: `overlay_dim_16` come tile/stretch su tutta la view

## Note VR
- Canvas in World Space, scale coerente (es. 0.001–0.002) e dimensioni testate a distanza reale.
- Preferisci TextMeshPro per testo nitido.
