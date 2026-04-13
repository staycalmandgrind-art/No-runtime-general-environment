# Ascendant Duel MVP Setup (Unity 2D)

This guide matches the current scripts in `Assets/Scripts` and is intentionally MVP-sized.

## 1) Scene setup

1. Create a new scene named `MVP_Arena`.
2. Add a **Ground** GameObject (Sprite + `BoxCollider2D`).
3. Set Ground to a layer named **Ground**.
4. Add a **Player** GameObject with:
   - `SpriteRenderer`
   - `Rigidbody2D` (Dynamic, freeze Z rotation)
   - `CapsuleCollider2D` (or `BoxCollider2D`)
   - `PlayerMovement`
   - `PlayerCombat`
5. Under Player, add empty child `GroundCheck` and place it at feet level.
6. In `PlayerMovement`, assign:
   - `Ground Check` = `GroundCheck`
   - `Ground Layer` = `Ground`
7. Create an **EnemyDummy** GameObject with:
   - `SpriteRenderer`
   - `BoxCollider2D`
   - `DummyTarget`
8. In `PlayerCombat`, drag `EnemyDummy` into `Current Target`.

## 2) Configure combat kits

In `PlayerCombat` inspector:

- `Kit 1` -> set 4 abilities (name, cooldown, damage)
- `Kit 2` -> set another 4 abilities

Example MVP values:

- Kit 1: Fire Bolt (3s), Dash Slash (5s), Guard Break (7s), Meteor Pop (9s)
- Kit 2: Ice Shot (3s), Wind Cut (4s), Earth Spike (6s), Thunder Mark (8s)

## 3) Keyboard test controls

- Move: `A/D` or arrow keys
- Jump: `Space`
- Light attack: `J` (no cooldown)
- Heavy attack: `K` (no cooldown)
- Abilities 1-4: `U`, `I`, `O`, `P` (cooldowns per active kit)
- Switch kit: `Left Shift`

## 4) Mobile UI wiring (basic)

1. Add a Canvas (Screen Space - Overlay).
2. Add a new GameObject named `MobileUIBinder` and attach `MobileCombatUIBinder`.
3. Assign `PlayerMovement` and `PlayerCombat` references in the binder.
4. For each button, add `OnClick` callback to binder:
   - Light -> `PressLightAttack`
   - Heavy -> `PressHeavyAttack`
   - Ability buttons -> `PressAbility1/2/3/4`
   - Kit Switch -> `PressKitSwitch`
   - Jump -> `PressJump`
5. For movement buttons (left/right), use EventTrigger for PointerDown/PointerUp:
   - Left down -> `SetMoveLeft(true)`
   - Left up -> `SetMoveLeft(false)`
   - Right down -> `SetMoveRight(true)`
   - Right up -> `SetMoveRight(false)`

## 5) Notes

- Light and heavy attacks are intentionally cooldown-free.
- Ability cooldowns are stored separately inside each kit.
- Switching kits swaps the active set of 4 ability slots.
