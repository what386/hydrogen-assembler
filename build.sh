#!/bin/bash

set -e # Exit on error

PROJECT_NAME="Assembler"
OUTPUT_DIR="./publish"
CONFIGURATION="Release"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

show_help() {
    echo "Usage: $0 <platform1> [platform2] ..."
    echo ""
    echo "Arguments:"
    echo "  <platforms>  Space-separated list of platforms to build"
    echo ""
    echo "Available platforms:"
    echo "  win-x64, win-x86, win-arm64"
    echo "  linux-x64, linux-arm64, linux-arm"
    echo "  osx-x64, osx-arm64"
    echo ""
    echo "Examples:"
    echo "  $0 win-x64                  # Build only Windows 64-bit"
    echo "  $0 win-x64 linux-x64        # Build Windows and Linux 64-bit"
    echo "  $0 osx-x64 osx-arm64        # Build both macOS versions"
    echo "  $0 win-x64 linux-x64 osx-arm64  # Build multiple platforms"
    exit 0
}

# Check for help flag or no arguments
if [[ "$1" == "-h" ]] || [[ "$1" == "--help" ]] || [ $# -eq 0 ]; then
    if [ $# -eq 0 ]; then
        echo -e "${RED}Error: No platforms specified${NC}"
        echo ""
    fi
    show_help
fi

BUILD_PLATFORMS=("$@")

echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}  Building ${PROJECT_NAME}${NC}"
echo -e "${GREEN}========================================${NC}"

# Clean output directory
if [ -d "$OUTPUT_DIR" ]; then
    echo -e "${YELLOW}Cleaning output directory...${NC}"
    rm -rf "$OUTPUT_DIR"
fi

mkdir -p "$OUTPUT_DIR"

build_platform() {
    local rid=$1
    local description=$2
    local platform_dir="$OUTPUT_DIR/$rid"

    echo ""
    echo -e "${YELLOW}Building for $description ($rid)...${NC}"

    dotnet publish ./src/$PROJECT_NAME/$PROJECT_NAME.csproj \
        -r "$rid" \
        --self-contained \
        -p:UseAppHost=true \
        -p:PublishSingleFile=True \
        -p:PublishTrimmed=True \
        -p:TrimMode=CopyUsed \
        -p:PublishReadyToRun=True \
        -o "${OUTPUT_DIR}/${rid}"

    if [ $? -eq 0 ]; then
        # Get file size
        if [[ "$rid" == win-* ]]; then
            exe_file="$platform_dir/${PROJECT_NAME}.exe"
        else
            exe_file="$platform_dir/${PROJECT_NAME}"
        fi

        if [ -f "$exe_file" ]; then
            size=$(du -h "$exe_file" | cut -f1)
            echo -e "${GREEN}✓ Built successfully ($size)${NC}"
        else
            echo -e "${RED}✗ Build completed but executable not found${NC}"
        fi
    else
        echo -e "${RED}✗ Build failed${NC}"
        return 1
    fi
}

get_platform_description() {
    local rid=$1
    case $rid in
    win-x64) echo "Windows (64-bit)" ;;
    win-x86) echo "Windows (32-bit)" ;;
    win-arm64) echo "Windows ARM64" ;;
    linux-x64) echo "Linux (64-bit)" ;;
    linux-arm64) echo "Linux ARM64" ;;
    linux-arm) echo "Linux ARM" ;;
    osx-x64) echo "macOS Intel" ;;
    osx-arm64) echo "macOS Apple Silicon" ;;
    *) echo "$rid" ;;
    esac
}

# Build selected platforms
echo -e "${BLUE}Building platforms: ${BUILD_PLATFORMS[*]}${NC}"

for rid in "${BUILD_PLATFORMS[@]}"; do
    description=$(get_platform_description "$rid")
    build_platform "$rid" "$description"
done

echo ""
echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}  Build Complete!${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""
echo "Executables are located in:"
echo ""

# Show directory structure
tree -L 2 "$OUTPUT_DIR" 2>/dev/null || find "$OUTPUT_DIR" -maxdepth 2 -type f -name "${PROJECT_NAME}*"
