import numpy as np
from matplotlib import rcParams
import matplotlib.pyplot as plt
import functions as f
from mpl_toolkits.mplot3d import Axes3D

# Figure Parameters:
#--------------------
SMALLEST_SIZE = 8
SMALL_SIZE  = 10
MEDIUM_SIZE = 11
BIGGER_SIZE = 12

rcParams['figure.figsize']  = (9,8)
rcParams['font.family']     = 'serif'    
rcParams['font.size']       = SMALL_SIZE           # controls default text sizes
rcParams['xtick.direction'] = 'out'
rcParams['ytick.direction'] = 'out'
rcParams['text.usetex']     = True
rcParams['xtick.labelsize'] = SMALLEST_SIZE
rcParams['ytick.labelsize'] = SMALLEST_SIZE
rcParams['lines.linewidth'] = 1
rcParams['axes.titlesize']  = SMALLEST_SIZE
rcParams['axes.labelsize']  = SMALL_SIZE
rcParams['legend.fontsize'] = SMALLEST_SIZE
rcParams['figure.titlesize']= BIGGER_SIZE
rcParams['hatch.linewidth'] = 0.4
rcParams['text.latex.preamble'] = [r'\usepackage{amsmath}']

for t in range(0,6001,10):
    data = f.readcsvfile('Burgers_Equation/bin/Debug/t=%d.csv' % t,delimiter=',',skip_lines=2)

    n = int(np.sqrt(len(data['x'])))

    x = data['x'].reshape([n,n])
    y = data['y'].reshape([n,n])
    u = data['u'].reshape([n,n])
    v = data['v'].reshape([n,n])
    spd = np.sqrt(u**2 + v**2)
    fig = plt.figure(figsize=(11, 7), dpi=100)
    axs = fig.gca(projection='3d')
    #cs = axs.contourf(x,y,spd,50,cmap=plt.cm.jet.reversed())
    cs = axs.plot_surface(x, y, spd, cmap=plt.cm.viridis, rstride=1, cstride=1)
    cbar = fig.colorbar(cs)
    axs.set_xlabel('x [m]')
    axs.set_xlabel('y [m]')
    axs.set_zlim(-0.1, 2.1)
    fig.savefig('Figures/Vel_%d.png' % t)



rcParams['figure.figsize']  = (12,8)
fig, axs = plt.subplots(2,3)
axs = axs.reshape(6,1)
S = ['Id','Id_l','Id_r','Id_t','Id_b']
for i in range(5):
    for j in range(len(data['x'])):
        axs[i,0].text(data['x'][j],data['y'][j],'%d' % data[S[i]][j],horizontalalignment='center',verticalalignment='center')
    axs[i,0].set_xlabel('x [m]')
    axs[i,0].set_ylabel('y [m]')
    axs[i,0].set_xlim(min(data['x'])-1,max(data['x'])+1)
    axs[i,0].set_ylim(min(data['y'])-1,max(data['y'])+1)
    axs[i,0].set_title(S[i].replace('_','-'))
fig.tight_layout()
fig.savefig('Figures/Ids.png')