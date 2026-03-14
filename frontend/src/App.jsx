import { useState, useEffect } from "react";

function App() {
  const [url, setUrl] = useState("");
  const [urls, setUrls] = useState([]);

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
    try {
      await fetch("/shorten-url", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ url: url }),
      });

      setUrl("");
      fetchUrls();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div style={{ fontFamily: "Arial", padding: "40px" }}>
      <h1>URL Shortener</h1>

      <input
        type="text"
        placeholder="Enter URL..."
        value={url}
        onChange={(e) => setUrl(e.target.value)}
        style={{ width: "300px", padding: "8px", marginRight: "10px" }}
      />

      <button onClick={shortenUrl}>Shorten</button>

      <div style={{ marginTop: "30px" }}>
        <h3>URLs Guardadas</h3>
        {urls.map((item) => (
          <div key={item.id} style={{ marginBottom: "10px", border: "1px solid #ccc", padding: "10px" }}>
            <p><strong>Original:</strong> <a href={item.urlOriginal}>{item.urlOriginal}</a></p>
            <p><strong>Corta:</strong> <a href={item.urlCorta}>{item.urlCorta}</a></p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default App;
