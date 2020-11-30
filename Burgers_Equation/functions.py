import numpy as np
from matplotlib import rcParams
import matplotlib.pyplot as plt

def readcsvfile(loc,skip_lines=1,columns_dtype='default',delimiter=','):
    '''
    reads a csv file and outputs the data as a dict. Each column of the csv file will become a key in the dict.

    loc = location of the file
    skip_lines = number of lines to skip to reach the title of each column.
    Note: skip_lines should not skip the line containing the title of each column.
    columns_dtype = 'default' if you want to use the default settings of np.genfromtxt
                or 'S8' for string with size 8
                or 'i8' for integer with size 8
                or 'f8' for float with size 8
                example a csv file with line: "a,b,c \n 12, 144.51, apples" ==> columns_dtype=[('a','i8',('b','f8'),('c',S8')]
    '''
    f = open(loc,'r')
    for i in range(skip_lines):
        garbage = f.readline()

    firstline = f.readline().decode("utf-8-sig").encode("utf-8")
    firstline = firstline.replace('\r','')
    firstline = firstline.split(',')

    names = [fr.split('\n')[0] for fr in firstline]
    csv_data= {}

    if columns_dtype == 'default':
        data = np.genfromtxt(loc,skip_header=skip_lines+1,delimiter=delimiter)

        for i in range(len(names)):
            csv_data.update({names[i]:data[:,i]})

    else:
        for c in columns_dtype:
            col_num = names.index(c[0])
            data = np.genfromtxt(loc,skip_header=skip_lines+1,dtype=c[1],usecols=(col_num),delimiter=delimiter)
            csv_data.update({c[0]:data})
        
    f.close()

    return  csv_data
