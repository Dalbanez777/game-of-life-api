# Game of Life API (.NET 8)

##  Problem Description

Implement Conway’s Game of Life as a RESTful API.  
The Game of Life is a zero-player game that evolves based on its initial state using simple rules on a grid of cells. The goal is to simulate the next generation(s) of a given board configuration.

##  Steps to Run Locally

### Prerequisites:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- VS Code or any C#-compatible editor
- `curl` or Postman for testing API endpoints

### 1. Clone and navigate to the project:
```bash
git clone <your-repo-url>
cd GameOfLife_NET8_WithSolution

### 2. Run the application:

dotnet run --project src/GameOfLife.csproj

### 3. Run Tests

dotnet test

The API will be running at:
https://localhost:5001 (HTTPS) or
http://localhost:5000 (HTTP)

### 4. Swagger UI:

Open your browser and go to:
http://localhost:5000/swagger

Explanation of the Solution and Thought Process
	•	The project follows a clean architecture structure: src/ for app logic and tests/ for tests.
	•	GameOfLifeService implements the main game logic including board validation and state transitions.
	•	State is stored in-memory using a Dictionary<Guid, Board> to simulate persistence.
	•	Controllers are thin and delegate logic to services.
	•	Swagger is integrated for API discoverability and easy testing.
	•	xUnit is used for unit testing core logic without relying on the API layer.

    Assumptions:
	•	All boards are rectangular and contain binary cell values (0 or 1).
	•	Input data is well-formed JSON and validated by the model.
	•	No persistence to disk or database — all data is in-memory.

Trade-offs:
	•	In-memory storage is fast and simple but not persistent across restarts.
	•	No API authentication or rate limiting — it’s focused purely on functionality.
	•	Performance is acceptable for small/medium boards but would need optimization for very large grids or high concurrency.

    ## Scalability Considerations

- In-memory storage can be replaced with a distributed cache (e.g., Redis) for multi-instance scaling.
- API is stateless and can be load-balanced easily.
- Logic is abstracted to support swapping in a database (e.g., MongoDB or SQL).
- Future versions can add board compression for reduced memory footprint on large grids.

## Designed for Extensibility

- Easily extendable to support user sessions or multiplayer patterns
- Replace in-memory board storage with persistent databases (e.g., SQL, NoSQL)
- REST endpoints can be expanded to include board history, analytics, or even live visualizations
.