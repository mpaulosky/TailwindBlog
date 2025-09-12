# Test Coverage Improvement Plan

This document summarizes current code coverage gaps (from coverage.opencover.xml) and proposes a prioritized, actionable plan to add tests that cover missing or under-tested code paths. The emphasis is on high-value areas (core handlers, layout logic, and user info flows) with minimal code changes.

Overall coverage (from report):
- Sequence coverage: 81.45%
- Branch coverage: 69.89%
- Classes covered: 85/100

## Highest Priority Targets

1) Web.Program (0% covered)
- Why: Entry-point configuration is important to validate DI, middleware, and endpoint wiring. Currently 0% coverage drags overall metrics.
- Proposed tests:
  - Startup smoke test using WebApplicationFactory or minimal host to ensure the app can build and start with default configuration.
  - Verify critical service registrations (e.g., auth, antiforgery, output caching) via service provider lookups.
- Suggested test files:
  - tests/Web.Tests.Unit/Startup/ProgramSmokeTests.cs
- Acceptance: At least one test executes code paths in Program (Main builder pipeline) without failing.

2) Web.Components.Layout.MainLayout.GetErrorCode (0% covered)
- Why: Logic mapping exceptions to HTTP-like status codes is pure, small, and easy to cover, giving quick branch coverage wins.
- Proposed tests:
  - Inputs: UnauthorizedAccessException, KeyNotFoundException, NotSupportedException, generic Exception, custom known exceptions if used.
  - Assert: returns expected codes (likely 401/403/404/500 or app-specific mapping) for each branch and default path.
- Suggested test files:
  - tests/Web.Tests.Unit/Components/Layout/MainLayoutTests.cs
- Acceptance: 100% sequence and branch coverage for GetErrorCode.

3) Web.Components.Features.UserInfo.Profile.LoadUserDataAsync (0% covered)
- Why: User profile loading is a meaningful user path. Current coverage is 0% for the state machine, indicating no tests exercise this async flow.
- Proposed tests (BUnit component tests):
  - Success path: authenticated user; mock Auth0 service to return user details and roles; assert rendered user info and roles list.
  - No user path: unauthenticated; assert appropriate placeholder/error UI.
  - Error path: Auth0 service throws or returns error; assert error UI and no crash.
- Infra:
  - Use Helpers.SetAuthorization (already used in repo) and DI registration for a mock IAuth0Service (or HttpClient via HttpMessageHandler stub if the service is concrete-only).
- Suggested test files:
  - tests/Web.Tests.Unit/Components/Features/UserInfo/Profile/ProfileTests.cs
- Acceptance: Cover branches in LoadUserDataAsync, including filters/linq display class delegate.

4) Category Handlers: GetCategories.Handler.HandleAsync (0% covered)
- Why: Core data retrieval logic used by Category list. Zero coverage means no direct validation of handler behavior.
- Proposed tests:
  - Returns Ok with list when repository has data.
  - Returns Ok with empty list when no data.
  - Propagates/handles exceptions gracefully (returns Fail with message and logs warning/error).
- Infra:
  - Mock IMyBlogContext (and IMongoCollection via abstraction or a fake provider) to return deterministic data.
- Suggested test files:
  - tests/Web.Tests.Unit/Components/Features/Categories/CategoryList/GetCategoriesHandlerTests.cs
- Acceptance: 100% sequence coverage for handler ctor and HandleAsync, meaningful branch coverage.

5) Category Handlers: EditCategory.Handler.HandleAsync (0% covered)
- Why: Update path should be reliable; editing failures need to be caught.
- Proposed tests:
  - Successfully updates existing category (Ok result).
  - Not found path (Fail with not found message).
  - Validation failure or exception path (Fail with appropriate error message); ensure logger invoked.
- Infra:
  - Mock IMyBlogContextFactory to provide a context; mock update behavior and exceptions.
- Suggested test files:
  - tests/Web.Tests.Unit/Components/Features/Categories/CategoryEdit/EditCategoryHandlerTests.cs
- Acceptance: Cover ctor and HandleAsync state machine branches.

## Medium Priority Targets

6) Web.Data.Auth0.Auth0Service helpers (static ctor, IgnoreUnderscoreNamingPolicy.ConvertName, lambdas)
- Current: Several inner classes/methods at 0%.
- Proposed tests:
  - Unit tests for IgnoreUnderscoreNamingPolicy.ConvertName covering names with/without leading underscore.
  - Test GetAccessTokenAsync error branches (already partially covered but branch coverage is 0% there)—provide failing HTTP responses (401/500) via HttpMessageHandler stub.
- Suggested test files:
  - tests/Shared.Tests.Unit/Data/Auth0/Auth0ServiceTests.cs
- Acceptance: Increase branch coverage for Auth0Service.

7) Web.Data.MyBlogContextFactory (0% covered)
- Proposed tests:
  - CreateAsync returns a context with initialized collections; cancels gracefully on CancellationToken cancellation.
- Suggested test files:
  - tests/Shared.Tests.Unit/Data/MyBlogContextFactoryTests.cs

## Low Priority / Consider Exclusions

8) Program.cs exclusion consideration
- If integration-sytle tests are not feasible in the unit test suite, consider applying [ExcludeFromCodeCoverage] on Program or refactor startup into a testable composition root method.

## Coverage Goals

- Web.dll sequence coverage: raise from 81.45% to ≥ 88%.
- Branch coverage: raise from 69.89% to ≥ 78%.
- Reduce 0%-covered classes by at least 70% (from 15 down to ≤ 5).

## Implementation Notes and Patterns

- BUnit patterns are already established in existing tests (Helpers.SetAuthorization, TestServiceRegistrations.RegisterCommonUtilities). Reuse these.
- For handlers relying on Mongo collections, favor testing via abstractions (IMyBlogContext) and returning in-memory lists to avoid heavy integration.
- Prefer Result<T> assertions (Ok/Fail) directly for handler tests; assert message text where applicable.
- Use deterministic fake data providers (FakeCategoryDto, FakeArticleDto) with useSeed=true for stable tests.
- Add logging assertions where feasible using a TestLogger or NSubstitute for ILogger.

## Proposed PR Slices (incremental)

- PR 1: Layout and Program smoke tests (MainLayoutTests + ProgramSmokeTests). Quick wins.
- PR 2: Category handlers tests (GetCategoriesHandlerTests + EditCategoryHandlerTests).
- PR 3: Profile component tests and Auth0 helpers.
- PR 4: MyBlogContextFactory tests and any residual gaps.

## Risks / Edge Cases to Cover

- CancellationToken behavior in async handlers/factories.
- Empty/null results from data layer without exceptions.
- Authentication state not yet available (null task) in components.
- HTTP failure modes for Auth0 service (timeouts, non-success status codes, malformed JSON).

## Next Steps

1) Create skeleton test classes in suggested locations with TODOs.
2) Implement PR 1 tests, run full suite, verify coverage gains.
3) Iterate through PR 2–4, monitoring sequence and branch coverage after each slice.