# Git Branching Strategy

We follow a simplified Git Flow model. All feature work happens on branches — never directly on `main`.

## Branch Types

- **main** — production-ready code only. Protected. Merges require at least one approved review.
- **develop** — integration branch. All feature branches merge here first.
- **feature/\*** — one branch per task or ticket (e.g. `feature/user-login`). Branch off from `develop`.
- **hotfix/\*** — urgent production fixes. Branch off from `main`, merge back into both `main` and `develop`.

## Naming Convention

Use lowercase and hyphens. Include the ticket number if one exists:

```
feature/42-password-reset
hotfix/missing-null-check
```

## Workflow

1. Pull the latest `develop`
2. Create your feature branch
3. Commit with descriptive messages (see Commit Message Guidelines below)
4. Push and open a Pull Request into `develop`
5. Assign at least one reviewer
6. Merge only after approval and passing CI checks

## Commit Message Guidelines

Use the imperative mood and keep the first line under 72 characters:

```
Add email validation to registration form
Fix null reference in order service
Remove deprecated payment gateway code
```

Avoid vague messages like "fix stuff" or "wip".
