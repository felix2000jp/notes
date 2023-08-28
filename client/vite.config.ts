import react from "@vitejs/plugin-react";
import path from "path";
import { defineConfig } from "vite";

export default defineConfig({
  server: {
    host: true,
    port: 80
  },
  resolve: { alias: [{ find: "~", replacement: path.resolve(__dirname, "src") }] },
  plugins: [react()]
});
