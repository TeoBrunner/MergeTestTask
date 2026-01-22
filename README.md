# 🧩 Merge UI
**A Unity merge puzzle game demonstrating Clean Architecture, Zenject, and custom UI systems.**

![Unity](https://img.shields.io/badge/Unity-2022.3.62f3-black?style=flat&logo=unity)
![Zenject](https://img.shields.io/badge/DI-Zenject-blue)
![DOTWeen](https://img.shields.io/badge/Animations-DOTween-purple)
![Platform](https://img.shields.io/badge/Platform-WebGL%20%7C%20Android-green)

https://github.com/user-attachments/assets/cef61d61-7309-408b-a2b7-50fda85e1131

[![Play](https://img.shields.io/badge/Play-Itch.io-orange)](https://teobrunner.itch.io/chip-merge)

## 📖 Overview
**Merge UI** is a test task implementation for a Unity Developer position, showcasing professional code architecture and custom system implementation. The project demonstrates the ability to create a fully functional merge puzzle game with a clean, maintainable codebase following SOLID principles.
This project serves as a technical showcase for:
- **Custom Drag & Drop system** without using Unity's built-in solutions
- **Dependency Injection** with Zenject
- **Decoupled architecture** with clear separation of concerns
- **Animation-driven UI** with DOTween
- **Custom input handling** without InputSystem
# 🎮 Game Design
Core Mechanics
- **Merge Puzzle:** Combine identical chips to create higher-level chips
- **3×3 Grid:** Playfield with 9 cells for chip placement
- **Drag & Drop:** Fully custom implementation using old Input system
- **Progression:** Chips level up through merging (Level 1 → 2 → 3...)
# Gameplay Flow 
1. **Spawn: **Click "Spawn" button to add random chips to empty cells
2. **Drag:** Click and drag chips between cells
3. **Merge:** Drop a chip on another identical chip to merge them
4. **Feedback:** Visual animations and floating messages for all actions

# 🏗 Architecture
The project follows **Clean Architecture** principles with strict separation between data, logic, and presentation layers. All systems are decoupled through interfaces and dependency injection.
## Core Components
1. **Domain Layer (Pure Logic)**
- `Board`: Manages the 3×3 grid and cell states
- `Cell`: Represents a single grid cell with chip occupancy logic
- `Chip`: Contains chip data (type, level) and merge logic
- `ChipType` & `ChipTypeDatabase`: ScriptableObject-based chip configuration   
3. **Service Layer (Business Logic)**
- `ISpawnService`: Handles random chip spawning
- `IMergeService`: Manages chip merging logic
- `IInputHandler`: Abstracts input system (MouseInputHandler implementation)
- `IMessageService`: Handles in-game notifications (FloatingMessageService)
4. **Presentation Layer (UI & Views)**
- `IBoardView`: Interface for board visualization
- `ICellView`: Interface for cell UI representation
- `IChipView`: Interface for chip visual representation
- `IDragDropHandler`: Custom drag & drop system implementation
5. **Infrastructure Layer (Systems)**
- `ViewRaycaster`: Custom GraphicRaycaster-based system for UI interaction
- `DragDropHandler`: Complete custom drag & drop implementation
- `GameController`: Orchestrates game flow and coordinates services
## Dependency Injection with Zenject
- `GameInstaller`: Central DI configuration
- **Factory Pattern**: `ChipView.Factory`, `CellView.Factory`, `FloatingMessageView.Factory`
- **Interface-based binding**: All systems bound through interfaces
- **Scene Context**: Manages scene-specific dependencies
- **Constructor Injection**: Primary method for service dependencies
## Key Design Patterns
- **Factory Method**: For creating views
- **Observer Pattern**: Event-driven drag & drop system
- **Strategy Pattern**: Different services for spawn/merge logic
- **Dependency Injection**: All components loosely coupled

# 🛠 Tech Stack & Implementation
## Custom Drag & Drop System
- **No Unity UI EventSystem**: Completely custom implementation
- **Old Input System**: Uses `Input.mousePosition` and mouse buttons
- **GraphicRaycaster**: Custom raycasting for UI element detection
- **CanvasGroup Management**: Dynamic raycast blocking during drag operations
## Animation System
- **DOTween Integration**: Smooth animations for all UI interactions
- **Sequence-based Animations**: Complex multi-step animations for merge effects
- **Easing Functions**: Professional easing for polish
## UI Architecture
- **MVC-inspired**: Views observe data models
- **Reactive Updates**: Views update when underlying data changes
- **Pooling-ready**: Factory pattern allows easy object pooling implementation
## Configuration System
- **ScriptableObjects**: For chip types and databases
- **Editor Integration**: `[CreateAssetMenu]` for easy asset creation
- **Type-safe References**: String ID system for chip type identification

# 📝 Key Implementation Details
## Custom Drag & Drop Highlights
- Completely independent of Unity's EventSystem triggers
- Uses GraphicRaycaster for accurate UI element detection
- Implements proper drag offsets and smoothing
- Handles all edge cases (cancel, invalid drops, same-cell drops)
## Animation Sequences
```cs
// Example merge animation sequence
currentAnimation = DOTween.Sequence()
    .Append(transform.DOScale(Vector3.one * mergeScale, mergeDuration * 0.5f))
    .Append(transform.DOScale(Vector3.one, mergeDuration * 0.5f))
    .OnComplete(() => onComplete?.Invoke());
```
## Zenject Binding Example
```cs
Container.Bind<IInputHandler>()
    .To<MouseInputHandler>()
    .AsSingle();

Container.Bind<IDragDropHandler>()
    .To<DragDropHandler>()
    .AsSingle();

Container.Bind<IMergeService>()
    .To<MergeService>()
    .AsSingle();
```

# 🎯 Project Goals Achieved
✅ **Custom Systems**: Full drag & drop without Unity's built-in solutions
✅ **Clean Architecture**: Strict separation of concerns with interfaces
✅ **Dependency Injection**: All components loosely coupled via Zenject
✅ **Professional Animations**: Smooth, polished UI with DOTween
✅ **Error Handling**: Comprehensive validation and error messages
✅ **Extensible Design**: Easy to add new chip types, animations, or features

# 🔧 Potential Extensions
1. **Object Pooling**: For chip view instances
2. **Sound System**: Audio feedback for merges and actions
3. **Score System**: Points for merges and combos
4. **Level Progression**: Increasing difficulty and chip types
5. **Save System**: Persist game state between sessions
6. **Undo System**: Reverse last move
7. **Hint System**: Suggest possible merges

*Built with attention to code quality, architecture, and user experience.*
