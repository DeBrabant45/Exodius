#!/bin/bash
set -euo pipefail

echo "====== Running Linux Browser Setup ======"

# Function to check if a command exists
command_exists() {
    command -v "$1" >/dev/null 2>&1
}

# Ensure keyrings directory exists
mkdir -p /etc/apt/keyrings

# === PowerShell ===
echo "Checking PowerShell..."
if command_exists pwsh; then
    echo "PowerShell is already installed."
else
    echo "Installing PowerShell..."
    wget -q https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
    dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    apt-get update
    apt-get install -y powershell
fi

# === Google Chrome ===
echo "Checking Google Chrome..."
if command_exists google-chrome || command_exists google-chrome-stable; then
    echo "Google Chrome is already installed."
else
    echo "Installing Google Chrome..."
    wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | \
        gpg --dearmor > /etc/apt/keyrings/google-linux-signing-keyring.gpg
    echo "deb [arch=amd64 signed-by=/etc/apt/keyrings/google-linux-signing-keyring.gpg] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list
    apt-get update
    apt-get install -y google-chrome-stable
fi

# === Microsoft Edge ===
echo "Checking Microsoft Edge..."
if command_exists microsoft-edge; then
    echo "Microsoft Edge is already installed."
else
    echo "Installing Microsoft Edge..."
    wget -q -O - https://packages.microsoft.com/keys/microsoft.asc | \
        gpg --dearmor > /etc/apt/keyrings/microsoft-edge.gpg
    echo "deb [arch=amd64 signed-by=/etc/apt/keyrings/microsoft-edge.gpg] https://packages.microsoft.com/repos/edge stable main" > /etc/apt/sources.list.d/microsoft-edge.list
    apt-get update
    apt-get install -y microsoft-edge-stable
fi

# === Mozilla Firefox ===
echo "Checking Mozilla Firefox..."
if command_exists firefox; then
    echo "Mozilla Firefox is already installed."
else
    echo "Installing Mozilla Firefox..."
    apt-get update
    apt-get install -y firefox
fi

echo
echo "====== Browser Setup Completed ======"