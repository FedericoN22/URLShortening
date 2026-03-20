import { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [url, setUrl] = useState("");
  const [urls, setUrls] = useState([]);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchUrls();
  }, []);

  const fetchUrls = async () => {
    try {
      const response = await fetch("/urls");
      const data = await response.json();
      setUrls(data);
    } catch (error) {
      console.error(error);
    }
  };

  const shortenUrl = async () => {
    setError("");
    setSuccess("");

    if (!url.trim()) {
      setError("Please enter a URL");
      return;
    }

    setLoading(true);

    try {
      const response = await fetch("/shorten-url", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ url: url }),
      });

      const data = await response.json();

      if (!response.ok) {
        setError(data.error || "Failed to shorten URL");
        return;
      }

      setSuccess("URL shortened successfully!");
      setUrl("");
      fetchUrls();

      setTimeout(() => setSuccess(""), 3000);
    } catch (error) {
      setError("Network error. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      shortenUrl();
    }
  };

  return (
    <div className="container">
      <header className="header">
        <h1>URL Shortener</h1>
        <p>Create short, shareable links in seconds</p>
      </header>

      <div className="input-section">
        <div className="input-wrapper">
          <input
            type="text"
            placeholder="Paste your URL here..."
            value={url}
            onChange={(e) => {
              setUrl(e.target.value);
              setError("");
            }}
            onKeyPress={handleKeyPress}
            className="url-input"
          />
          <button 
            onClick={shortenUrl} 
            disabled={loading}
            className="btn-primary"
          >
            {loading ? "Shortening..." : "Shorten"}
          </button>
        </div>

        {error && (
          <div className="alert alert-error">
            <span className="alert-icon">✕</span>
            {error}
          </div>
        )}

        {success && (
          <div className="alert alert-success">
            <span className="alert-icon">✓</span>
            {success}
          </div>
        )}
      </div>

      <div className="urls-section">
        <h2>Saved URLs</h2>
        {urls.length === 0 ? (
          <p className="empty-state">No URLs saved yet</p>
        ) : (
          <div className="urls-list">
            {urls.map((item) => (
              <div key={item.id} className="url-card">
                <div className="url-info">
                  <span className="url-label">Original</span>
                  <a href={item.urlOriginal} target="_blank" rel="noopener noreferrer" className="url-link">
                    {item.urlOriginal.length > 50 
                      ? item.urlOriginal.substring(0, 50) + "..." 
                      : item.urlOriginal}
                  </a>
                </div>
                <div className="url-info">
                  <span className="url-label">Short</span>
                  <span className="short-code">{item.urlCorta}</span>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
