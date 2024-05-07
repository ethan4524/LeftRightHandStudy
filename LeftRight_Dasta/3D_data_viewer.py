import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import tkinter as tk
from tkinter import ttk
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

# Read the CSV file into a DataFrame
logger_data_df = pd.read_csv('logger_data.csv')
player_data_df = pd.read_csv('player_data.csv')

unique_player_ids = player_data_df['Session ID'].unique()
player_names = {}
for i, player_id in enumerate(unique_player_ids):
    player_names[f'Player {i+1}'] = player_id

# Convert timeStamp column to datetime format
logger_data_df['timeStamps'] = pd.to_datetime(logger_data_df['timeStamps'])

# Get the list of column headers for dropdown options
column_options = list(logger_data_df.columns)

# Create the GUI
root = tk.Tk()
root.title("Player Data Analyzer")

# Function to update the plot based on the selected player, headers, and time range
def update_plot():
    # Clear the existing plot
    ax.clear()
    
    # Get selected player ID
    selected_player_id = player_names[player_var.get()]
    
    # Get start and end numbers from text boxes
    start_number = int(start_entry.get())
    end_number = int(end_entry.get())
    
    # Get selected columns for Y and Z axes
    y_column = y_axis_var.get()
    z_column = z_axis_var.get()
    
    # Filter the DataFrame based on selected player and time range
    player_data = logger_data_df[(logger_data_df['playerIDs'] == selected_player_id) &
                                  (logger_data_df.index >= start_number) &
                                  (logger_data_df.index <= end_number)]
    
    # Plot the selected data on the specified axes
    ax.plot(player_data.index, player_data[y_column], player_data[z_column], label='Data')
    
    # Set plot labels and title
    ax.set_xlabel('Index')
    ax.set_ylabel(y_column)
    ax.set_zlabel(z_column)
    ax.set_title('Spacial Data Plotter')
    
    # Add legend
    ax.legend()
    
    # Redraw canvas
    canvas.draw()

# Function to quit the application
def quit_app():
    root.quit()
    root.destroy()

# Create a frame for selecting player, time range, and headers
selection_frame = tk.Frame(root)
selection_frame.pack()

# Dropdown menu for selecting player
player_var = tk.StringVar(root)
player_var.set('Player 1')  # Default value
player_dropdown = ttk.Combobox(selection_frame, textvariable=player_var, values=list(player_names.keys()))
player_dropdown.pack(side=tk.LEFT)

# Create labels and entry widgets for start and end numbers
start_label = tk.Label(selection_frame, text="Start:")
start_label.pack(side=tk.LEFT)
start_entry = tk.Entry(selection_frame)
start_entry.pack(side=tk.LEFT)

end_label = tk.Label(selection_frame, text="End:")
end_label.pack(side=tk.LEFT)
end_entry = tk.Entry(selection_frame)
end_entry.pack(side=tk.LEFT)

# Dropdown menus for selecting columns for Y and Z axes
header_frame = tk.Frame(root)
header_frame.pack()

y_axis_var = tk.StringVar(root)
y_axis_var.set(column_options[0])  # Default value
y_axis_dropdown = ttk.Combobox(header_frame, textvariable=y_axis_var, values=column_options)
y_axis_dropdown.pack(side=tk.LEFT)

z_axis_var = tk.StringVar(root)
z_axis_var.set(column_options[1])  # Default value
z_axis_dropdown = ttk.Combobox(header_frame, textvariable=z_axis_var, values=column_options)
z_axis_dropdown.pack(side=tk.LEFT)

# Create the "Update Plot" button
update_button = tk.Button(root, text="Update Plot", command=update_plot)
update_button.pack()

# Create a quit button
quit_button = tk.Button(root, text="Quit", command=quit_app)
quit_button.pack()

# Create a frame for the plot
plot_frame = tk.Frame(root)
plot_frame.pack()

# Create a canvas for the plot
fig = plt.figure(figsize=(20, 10))
ax = fig.add_subplot(111, projection='3d')

# Run the GUI
canvas = FigureCanvasTkAgg(fig, master=plot_frame)
canvas.draw()
canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)

# Run the GUI
root.mainloop()
