# Project Board

A lightweight desktop project board for organising tasks and notes within a single project file.

This tool was built to track small projects where tasks have dependencies and progress needs to be visible at a glance.
It combines task management with markdown notes so both planning and implementation context live in the same place.

Each project is saved as a **single file**, making it easy to store, move, or version.

---

## Overview

Project Board manages two types of content within a project:

* **Notes** - markdown documents for design, research, or documentation.
* **Tasks** - uniquely named work items with tags, dependencies, statuses, priorities, and alerts.

The goal is to make it easy to see **what can be worked on right now**, while still keeping contextual information
nearby.

---

## Demo

https://youtu.be/QqcBvehihdg

---

## Screenshots

![Main menu](https://github.com/fraserelliott/project-board/blob/master/readme-images/main%20menu.png)
![Tasks screen](https://github.com/fraserelliott/project-board/blob/master/readme-images/tasks%20screen.png)
![Task details window](https://github.com/fraserelliott/project-board/blob/master/readme-images/task%20details.png)
![Notes screen](https://github.com/fraserelliott/project-board/blob/master/readme-images/notes%20screen.png)

---

## Key Features

### Projects

* Each project is stored as a **single file**
* Projects contain both **tasks and notes**
* Designed to be lightweight and easy to move or archive

---

### Notes

Notes are markdown documents stored inside the project.

Features:

* Any number of notes per project
* Each note has a **name and editable content**
* Notes render using **Markdown**
* Built-in **search bar** searches:

    * note titles
    * note contents

This makes notes useful for design documentation, architecture notes, or planning.

---

### Tasks

Tasks represent units of work within the project.

Each task has:

* A **unique name**
* **Description** (supports Markdown)
* **Priority** (any integer)
* **Tags**
* **Dependencies**
* **Status**

Tasks can be viewed in a board-style list with quick actions.

---

### Task Status Workflow

Tasks follow a simple state model:

```
NotStarted → Started → Completed
               ↑         ↓
               └─ Reopen ┘
```

Available actions:

* **Start**
* **Complete**
* **Reopen**

Status transitions are intentionally minimal to keep task tracking simple.

---

### Tags

Tags allow tasks to be grouped or categorised.

* Tags are **global to the project**
* Tasks can attach **any number of tags**
* Tags can be used to visually organise work in the board

---

### Task Dependencies

Tasks can depend on other tasks.

This allows modelling workflows where work must be completed in a specific order.

Features:

* Dependencies are **validated to prevent cycles**
* A task **cannot depend on itself indirectly**
* Invalid dependencies are **never presented as options**

This guarantees the dependency graph always remains valid.

---

### Task Alerts

The system highlights tasks that may need attention.

#### Blocked

A task is **blocked** if:

* It is **not completed**, and
* It has a dependency that is **incomplete or blocked**

Blocked tasks:

* Cannot change status
* Clearly indicate that prerequisite work is required

---

#### Stale

A task becomes **stale** if:

* It is **completed**, and
* A dependency becomes **incomplete or stale**

This indicates that a previously completed task may need review.

---

### Task Details

Each task has a dedicated details view.

The details page shows:

* Description (Markdown)
* Tags
* Dependencies
* Priority
* Alerts
* Blocking or stale dependencies

This makes it easy to understand **why a task is blocked or stale** and what needs to be addressed.

---

### Filtering

The task board includes quick filters to hide:

* Completed tasks
* Blocked tasks
* Stale tasks

This helps focus on **work that can be acted on immediately**.

---

## Goals

This project was built to explore:

* desktop UI design
* MVVM architecture
* task dependency modelling
* simple project management workflows

The focus is on **clarity and usability**, rather than feature-heavy project management.

---

## Architecture Overview

Project Board follows a **Model–View–ViewModel (MVVM)** architecture.

The goal of the architecture is to keep domain logic, UI behaviour, and presentation clearly separated.

### Models

Models represent the core project data.

Key models include:

* **Project**

    * The root object containing tasks and notes
    * Responsible for saving and loading the project file

* **Task**

    * Unique task name
    * Status
    * Priority
    * Tags
    * Dependencies
    * Description

* **Note**

    * Named document
    * Markdown content

The models contain the **business rules** for tasks, including dependency validation and alert logic.

---

### ViewModels

ViewModels provide the bridge between the UI and the domain models.

Responsibilities include:

* exposing observable properties for UI binding
* implementing commands for user actions (start, complete, reopen, edit, etc.)
* coordinating filtering and search
* translating model state into UI-friendly representations

Examples include:

* **TaskItemViewModel** – represents a task in the board
* **TaskDetailsViewModel** – powers the task details page
* **NotesViewModel** – manages notes and note search

---

### Views

Views are implemented using WPF and are responsible only for presentation.

Features include:

* task board view
* task details window
* markdown rendering for notes and descriptions
* filtering controls

The UI uses data binding so that updates to ViewModels automatically reflect in the interface.

---

## Design Decisions

### Dependency-Based Task Organisation

The core idea behind Project Board is that **task dependencies define what work is possible at any given moment**.

Instead of manually ordering tasks or maintaining a strict sequence, tasks declare which other tasks they depend on.

This allows the system to determine:

* which tasks can currently be worked on
* which tasks are blocked
* when previously completed work might need revisiting

---

### Alerts Instead of Strict Workflow Enforcement

Rather than enforcing complex workflows, the system surfaces **alerts**:

* **Blocked** - the task cannot currently be worked on
* **Stale** - completed work may need revisiting

This approach keeps the workflow flexible while still making the dependency structure visible.

---

### Focus on Actionable Work

The task board is designed to answer a simple question:

> **What can I work on right now?**

By combining task dependencies, alerts, and filtering, the board helps highlight tasks that are currently actionable
without requiring the user to manually manage ordering or sequencing.

---

## Future Ideas

Some possible areas for future exploration:

* attachments (e.g. wireframes or images)
* additional filtering and sorting
* improved note linking