import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import tkinter as tk
from tkinter import ttk
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg


# Function to calculate running average with a window size of 3
def running_average(data):
    return data.rolling(window=2, min_periods=1).mean()

# Function to update the plot based on the start and end values entered
def update_plot():
    # Get start and end numbers from text boxes
    start_number = int(start_entry.get())
    end_number = int(end_entry.get())
    
    # Filter the DataFrame based on start and end numbers
    player_data = logger_data_df.iloc[start_number:end_number]
    
    # Calculate running average for right hand y position
    player_data['rightHandPos_y_avg'] = running_average(player_data['rightHandPos_y'])
    
    # Clear the existing plot
    ax.clear()
    
    # Plot the new data and running average
    ax.plot(range(len(player_data)), player_data['rightHandPos_z'], player_data['rightHandPos_y'], label='Right Hand')
    ax.plot(range(len(player_data)), player_data['rightHandPos_z'], player_data['rightHandPos_y_avg'], label='Running Average')
    
    # Update plot labels, title, etc. if needed
    
    # Redraw canvas
    canvas.draw()

# Read the CSV file into a DataFrame
logger_data_df = pd.read_csv('logger_data.csv')

# Convert timeStamp column to datetime format
logger_data_df['timeStamp'] = pd.to_datetime(logger_data_df['timeStamp'])

# Filter data for a specific player (e.g., playerID = 'b1a54a67-ec21-4958-af34-2352bdbe3f10')
player_id = 'b1a54a67-ec21-4958-af34-2352bdbe3f10'
player_data = logger_data_df[logger_data_df['playerID'] == player_id]
player_data = player_data.iloc[2100:2300]

# Create the GUI
root = tk.Tk()
root.title("Player Data Analyzer")

# Create a frame for the plot
plot_frame = tk.Frame(root)
plot_frame.pack()

# Create a canvas for the plot
fig = plt.figure(figsize=(15, 10))
ax = fig.add_subplot(111, projection='3d')
canvas = FigureCanvasTkAgg(fig, master=plot_frame)
canvas.draw()
canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)

# Create labels and entry widgets for start and end numbers
input_frame = tk.Frame(root)
input_frame.pack()

start_label = tk.Label(input_frame, text="Start:")
start_label.pack(side=tk.LEFT)
start_entry = tk.Entry(input_frame)
start_entry.pack(side=tk.LEFT)

end_label = tk.Label(input_frame, text="End:")
end_label.pack(side=tk.LEFT)
end_entry = tk.Entry(input_frame)
end_entry.pack(side=tk.LEFT)

# Create the "Update Plot" button
update_button = tk.Button(root, text="Update Plot", command=update_plot)
update_button.pack()

# Run the GUI
root.mainloop()