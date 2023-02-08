import seaborn as sns
import matplotlib.pyplot as plt
from matplotlib.colors import LogNorm

OUTCOME_ENCODES = {'HEALTHAFF':0,
                   'AL1CHILDQUIET':1,
                   'AL1CHILDAGGR':2,
                   'AL1CHILDBEDW':3,
                   'AL1CHILDNIGHT':4,
                   'AL1CHILDRSCH':5,
                   'AL1CHILDDSCH':6,
                   'DISWORK':7,
                   'SUICIDEATT':8,
                   'ABOR':9,
                   'MISC':10,
                   'STILB':11,
                   'HEALTHSTA':12,
                   'HURT':13,
                   'INJ':14,
                   'LOSCONF':15,
                   'LOSCONS':16,
                   'PAIN':17,
                   'PROBWALK':18,
                   'PROBMEM':19,
                   'PROBUSUAL':20,
                   'EMODIST':21,
                   'SUICIDETHI':22,
                   'CONCENTRATE':23,
                   'WORK':24,
                   'ANY':25}


def heatmaps_side_by_side(df1, df2, title, cmap):
    """
    This function plots two heatmaps side by side.

    Inputs:
    df1 (DataFrame): The first DataFrame to be plotted.
    df2 (DataFrame): The second DataFrame to be plotted.
    title (str): The title for the plot.
    cmap (str): The color map for the heatmap.
    
    Output:
    A plot of the two heatmaps.
    """

    # Create a subplot with two heatmaps
    fig, (ax1, ax2) =plt.subplots(nrows=1,ncols=2, figsize=(18,13))
    # Add title to the subplot
    fig.suptitle(title)
    # Plot first heatmap on the left
    sns.heatmap(df1,
                annot=True,
                norm=LogNorm(),
                cmap=cmap,
                fmt='.0f',
                ax=ax1
               )
    # Plot second heatmap on the right
    sns.heatmap(df2,
                annot=True,
                norm=LogNorm(),
                cmap=cmap,
                fmt='.0f',
                ax=ax2
               )
    # Show the subplot
    plt.show()