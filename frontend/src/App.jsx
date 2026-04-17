import { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [url, setUrl] = useState("");
  const [urls, setUrls] = useState([]);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [loading, setLoading] = useState(false);
  const [editingId, setEditingId] = useState(null);
  const [editUrl, setEditUrl] = useState("");

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

  const handleDelete = async (id) => {
    if (!confirm("Are you sure you want to delete this URL?")) return;

    try {
      const response = await fetch(`/urls/${id}`, {
        method: "DELETE",
      });

      if (response.ok) {
        setSuccess("URL deleted successfully!");
        fetchUrls();
        setTimeout(() => setSuccess(""), 3000);
      }
    } catch (error) {
      setError("Failed to delete URL");
    }
  };

  const handleEdit = (item) => {
    setEditingId(item.shortUrl);
    setEditUrl(item.originalUrl);
  };

  const handleUpdate = async (id) => {
    setError("");

    if (!editUrl.trim()) {
      setError("Please enter a valid URL");
      return;
    }

    try {
      const response = await fetch(`/urls/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ url: editUrl }),
      });

      const data = await response.json();

      if (!response.ok) {
        setError(data.error || "Failed to update URL");
        return;
      }

      setSuccess("URL updated successfully!");
      setEditingId(null);
      setEditUrl("");
      fetchUrls();
      setTimeout(() => setSuccess(""), 3000);
    } catch (error) {
      setError("Failed to update URL");
    }
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setEditUrl("");
    setError("");
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
              <div key={item.shortUrl} className="url-card">
                {editingId === item.shortUrl ? (
                  <div className="edit-form">
                    <input
                      type="text"
                      value={editUrl}
                      onChange={(e) => setEditUrl(e.target.value)}
                      className="url-input edit-input"
                    />
                    <div className="edit-actions">
                      <button onClick={() => handleUpdate(item.shortUrl)} className="btn-save">
                        Save
                      </button>
                      <button onClick={handleCancelEdit} className="btn-cancel">
                        Cancel
                      </button>
                    </div>
                  </div>
                ) : (
                  <>
                    <div className="url-info">
                      <span className="url-label">Original</span>
                      <a href={item.originalUrl} target="_blank" rel="noopener noreferrer" className="url-link">
                        {item.originalUrl && item.originalUrl.length > 50 
                          ? item.originalUrl.substring(0, 50) + "..." 
                          : item.originalUrl}
                      </a>
                    </div>
                    <div className="url-info">
                      <span className="url-label">Short</span>
                      <a href={`/r/${item.shortUrl}`} target="_blank" rel="noopener noreferrer" className="short-code">
                        {item.shortUrl}
                      </a>
                    </div>
                    <div className="url-info">
                      <span className="url-label">Clicks</span>
                      <span className="click-count">{item.clicks}</span>
                    </div>
                    <div className="url-actions">
                      <button onClick={() => handleEdit(item)} className="btn-edit">
                        Edit
                      </button>
                      <button onClick={() => handleDelete(item.shortUrl)} className="btn-delete">
                        Delete
                      </button>
                    </div>
                  </>
                )}
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
