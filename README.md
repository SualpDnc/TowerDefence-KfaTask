	Combat system:
	•	Melee or ranged attacks (switchable via UI).
	•	Each attack type has its own animations, sounds, and visual effects.
	•	Blood particle effects for melee hits.
	Health system:
	•	Player health displayed in UI.
	•	Invulnerability frames (I-frames) after taking damage.
	Wave system:
	•	Enemies spawn in configurable waves (all values editable in Inspector).
	•	Boss enemies appear on waves 5, 10, 15.
	•	Option to call next wave early via UI button.
	Enemies:
	•	Follow a predefined path.
	•	Simple animations and attack triggers.
	•	If 5 enemies reach the end point → Game Over.
	UI:
	•	Start screen.
	•	Wave counter.
	•	Enemy slip counter (X/5).
	•	Game Over screen with custom message.
	•	Victory screen when all waves are cleared.
	Audio & VFX:
	•	Background music loop.
	•	Attack, hit, and walk sounds.
	•	Particle effects for ranged and melee hits.

⸻

🛠️ Tech Stack
	•	Engine: Unity (2021+ recommended)
	•	Language: C#
	•	Art: 2D sprites with billboard rendering in a 3D world
	•	Sound & VFX: Unity Particle System + AudioSource
