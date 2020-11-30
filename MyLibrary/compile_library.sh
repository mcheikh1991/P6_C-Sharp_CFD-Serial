#!/bin/bash

mcs -t:library Point.cs Vector.cs Grid_Point.cs Domain.cs -out:MyLibrary.dll
