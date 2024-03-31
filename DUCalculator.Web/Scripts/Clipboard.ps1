# Path to the file where clipboard content will be appended
$file = "clipboard_content.txt"

$lastValue = ""
$counter = 1

# Continuously monitor the clipboard and append new content to the file
while ($true) {
    # Get clipboard content
    $clipboardContent = Get-Clipboard -Format Text

    # Check if clipboard content is not empty
    if ($null -ne $clipboardContent) {
        # Append clipboard content to the file  

        if($lastValue -ne $clipboardContent)
        {
            $lastValue = $clipboardContent

            Add-Content -Path $file -Value "WP$counter $clipboardContent"
            Write-Host "$clipboardContent"

            $counter++
        }
    }

    # Wait for some time before checking the clipboard again
    Start-Sleep -Seconds 1  # Adjust the delay as needed
}
