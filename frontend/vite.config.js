import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/urls': 'http://localhost:5254',
      '/shorten-url': 'http://localhost:5254'
    }
  }
})
