import pandas as pd
from datetime import datetime

# Define the path to your data file
file_path = "data.txt"

# Initialize lists to store data
timeStamps = []
sessionIDs = []
handsShown = []
handedness = []
roundCounters = []
playerGuesses = []
correctAnswers = []
currentGuessTimes = []

# Read data from file
with open(file_path, "r") as file:
    lines = file.readlines()

# Parse each line and extract data
for i in range(0, len(lines), 8):
    timeStamp = datetime.strptime(lines[i].strip(), "%m/%d/%Y %I:%M:%S %p")
    sessionID = lines[i+1].strip()
    handsShown.append(eval(lines[i+2].strip()))  # Convert string "True" to boolean True
    handedness.append(lines[i+3].strip())
    roundCounter = int(lines[i+4].strip())
    playerGuess = lines[i+5].strip()
    correctAnswer = lines[i+6].strip()
    currentGuessTime = float(lines[i+7].strip())

    # Append data to lists
    timeStamps.append(timeStamp)
    sessionIDs.append(sessionID)
    roundCounters.append(roundCounter)
    playerGuesses.append(playerGuess)
    correctAnswers.append(correctAnswer)
    currentGuessTimes.append(currentGuessTime)

# Create DataFrame
player_data_df = pd.DataFrame({
    "timeStamp": timeStamps,
    "Session ID": sessionIDs,
    "handsShown": handsShown,
    "handedness": handedness,
    "roundCounter": roundCounters,
    "player guess": playerGuesses,
    "correct answer": correctAnswers,
    "currentGuessTime": currentGuessTimes
})

# Save DataFrame as CSV
player_data_df.to_csv("player_data.csv", index=False)

# Display the DataFrame
print(player_data_df)