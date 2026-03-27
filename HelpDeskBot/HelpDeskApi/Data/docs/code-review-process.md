# Code Review Process

Code reviews are mandatory for all changes merged into `develop` or `main`. They serve both as a quality gate and a knowledge-sharing opportunity.

## Opening a Pull Request

- Keep PRs focused. One PR should address one concern — a feature, a bug fix, or a refactor. Not all three.
- Write a clear PR description: what changed, why, and how to test it.
- Link the relevant ticket number in the description.
- Assign at least one reviewer. For changes to core services, assign two.

## Reviewer Responsibilities

- Review within **one business day** of being assigned.
- Check for correctness, readability, and potential side effects — not just style.
- Leave constructive comments. If something must change, use "Request changes". If it's a suggestion, prefix it with "Nit:" so the author knows it's optional.
- Approve only when you're confident the change is safe to merge.

## Author Responsibilities

- Respond to every comment, even if just to acknowledge it.
- Do not merge until all "Request changes" reviews are resolved.
- Do not force-push after review has started — it invalidates existing comments.

## What Reviewers Look For

- Does the code do what the ticket describes?
- Are edge cases handled?
- Are there any obvious performance or security concerns?
- Is the new code covered by tests?
- Does it follow our existing patterns and conventions?

## Merging

Use **Squash and Merge** for feature branches to keep the `develop` history clean. For hotfixes, use a regular merge to preserve the commit context.
