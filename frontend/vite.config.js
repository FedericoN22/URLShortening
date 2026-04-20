import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/urls': 'http://localhost:5000',
      '/shorten-url': 'http://localhost:5000',
      '/r': 'http://localhost:5000',
      '/stats': 'http://localhost:5000',
      '/debug': 'http://localhost:5000'
    }
  }
})
