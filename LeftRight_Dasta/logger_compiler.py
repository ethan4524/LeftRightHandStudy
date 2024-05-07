import pandas as pd
from datetime import datetime

# Define the path to your logger file
file_path = "logger.txt"

# Initialize lists to store data
playerIDs = []
timeStamps = []
spacePos_x = []
spacePos_y = []
spacePos_z = []
headPos_x = []
headPos_y = []
headPos_z = []
headRot_x = []
headRot_y = []
headRot_z = []
leftHandPos_x = []
leftHandPos_y = []
leftHandPos_z = []
leftHandRot_x = []
leftHandRot_y = []
leftHandRot_z = []
rightHandPos_x = []
rightHandPos_y = []
rightHandPos_z = []
rightHandRot_x = []
rightHandRot_y = []
rightHandRot_z = []

# Read data from file
with open(file_path, "r") as file:
    lines = file.readlines()

# Parse each line and extract data
for i in range(0, len(lines), 42):
    playerID = lines[i].strip()
    timeStamp = lines[i + 1].strip()
    spacePos_x.append(float(lines[i + 2].strip()))
    spacePos_y.append(float(lines[i + 3].strip()))
    spacePos_z.append(float(lines[i + 4].strip()))
    headPos_x.append(float(lines[i + 5].strip()))
    headPos_y.append(float(lines[i + 6].strip()))
    headPos_z.append(float(lines[i + 7].strip()))
    headRot_x.append(float(lines[i + 14].strip()))
    headRot_y.append(float(lines[i + 15].strip()))
    headRot_z.append(float(lines[i + 16].strip()))
    leftHandPos_x.append(float(lines[i + 8].strip()))
    leftHandPos_y.append(float(lines[i + 9].strip()))
    leftHandPos_z.append(float(lines[i + 10].strip()))
    leftHandRot_x.append(float(lines[i + 29].strip()))
    leftHandRot_y.append(float(lines[i + 30].strip()))
    leftHandRot_z.append(float(lines[i + 31].strip()))
    rightHandPos_x.append(float(lines[i + 11].strip()))
    rightHandPos_y.append(float(lines[i + 12].strip()))
    rightHandPos_z.append(float(lines[i + 13].strip()))
    rightHandRot_x.append(float(lines[i + 32].strip()))
    rightHandRot_y.append(float(lines[i + 33].strip()))
    rightHandRot_z.append(float(lines[i + 34].strip()))

    # Append player ID and timestamp to lists
    playerIDs.extend([playerID] * 3)  # Extend the list with the player ID repeated 3 times
    timeStamps.extend([timeStamp] * 3)  # Extend the list with the timestamp repeated 3 times

# Create DataFrame
logger_data_df = pd.DataFrame({
    "playerID": playerIDs,
    "timeStamp": timeStamps,
    "spacePos_x": spacePos_x,
    "spacePos_y": spacePos_y,
    "spacePos_z": spacePos_z,
    "headPos_x": headPos_x,
    "headPos_y": headPos_y,
    "headPos_z": headPos_z,
    "headRot_x": headRot_x,
    "headRot_y": headRot_y,
    "headRot_z": headRot_z,
    "leftHandPos_x": leftHandPos_x,
    "leftHandPos_y": leftHandPos_y,
    "leftHandPos_z": leftHandPos_z,
    "leftHandRot_x": leftHandRot_x,
    "leftHandRot_y": leftHandRot_y,
    "leftHandRot_z": leftHandRot_z,
    "rightHandPos_x": rightHandPos_x,
    "rightHandPos_y": rightHandPos_y,
    "rightHandPos_z": rightHandPos_z,
    "rightHandRot_x": rightHandRot_x,
    "rightHandRot_y": rightHandRot_y,
    "rightHandRot_z": rightHandRot_z
})

# Display the DataFrame
print(logger_data_df)
logger_data_df.to_csv('logger_data.csv', index=False)
