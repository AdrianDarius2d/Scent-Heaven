// Smoke test: confirms the frontend can reach the API.
fetch("/api/health")
  .then(r => r.json())
  .then(d => document.getElementById("status").textContent = "API status: " + d.status)
  .catch(() => document.getElementById("status").textContent = "API not reachable");
