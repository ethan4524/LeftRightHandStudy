import pandas as pd

# Define column names
columns = ['playerIDs', 'timeStamps', 'spacePos_x', 'spacePos_y', 'spacePos_z',
           'rig_rot_y', 'headPos_x', 'headPos_y', 'headPos_z', 'headFor_x',
           'headFor_y', 'headFor_z', 'headUp_x', 'headUp_y', 'headUp_z',
           'headRot_x', 'headRot_y', 'headRot_z', 'leftHandPos_x', 'leftHandPos_y',
           'leftHandPos_z', 'leftHandFor_x', 'leftHandFor_y', 'leftHandFor_z',
           'leftHandUp_x', 'leftHandUp_y', 'leftHandUp_z', 'leftHandRot_x',
           'leftHandRot_y', 'leftHandRot_z', 'rightHandPos_x', 'rightHandPos_y',
           'rightHandPos_z', 'rightHandFor_x', 'rightHandFor_y', 'rightHandFor_z',
           'rightHandUp_x', 'rightHandUp_y', 'rightHandUp_z', 'rightHandRot_x',
           'rightHandRot_y', 'rightHandRot_z']

# Initialize empty dictionary to store data
data = {col: [] for col in columns}

# Read the file
with open('logger.txt', 'r') as file:
    lines = file.readlines()

# Iterate through each line and assign values to columns
for idx, line in enumerate(lines):
    data[columns[idx % len(columns)]].append(line.strip())

# Create DataFrame
df = pd.DataFrame(data)

# Save DataFrame to CSV

df.to_csv('logger_data.csv', index=False)
