
# coding: utf-8

# In[1]:
import pylint
import csv
import sys

import pandas as pd #Dataframe, Series
import numpy as np #Scientific Computing

from sklearn import tree
from sklearn.tree import DecisionTreeClassifier, export_graphviz
from sklearn.model_selection import train_test_split

from matplotlib import pyplot as plt
import seaborn as sns
import io
import imageio
import graphviz
import pydotplus
from scipy import misc
import PIL


# In[2]:


data = pd.read_csv('C:/Users/JasonTjauw/Documents/Project 78/RickyKech.csv')
data2 = pd.read_csv('C:/Users/JasonTjauw/Downloads/FinalTest.csv')
#data2 = pd.read_csv('C:/Users/JasonTjauw/Downloads/FinalTest.csv')


# In[3]:


data.describe()

# In[4]:



data.head()


# In[5]:


train, test = train_test_split(data, test_size = 0.15)
train2, test2 = train_test_split(data2, test_size = len(data2) - 1)


# In[6]:


print("Training size{}: Test size: {}".format(len(train), len(test)))


# In[7]:


train.shape


# In[8]:


c = DecisionTreeClassifier(criterion="entropy", min_samples_split=85)


# In[9]:


features = ['itvid','itvRegieParentId','itvTargetId','probId', 'lgscoreId', 'lgId', 'casId', 'tgId', 'casTargetId', 'ptid', 'sclSubjectId', 'sclId', 'sclCollectionId', 'probprobleemoptieId']


# In[10]:


X_train = train[features]
y_train = train['itvInterventieOptieId']

x_test = test[features]
y_test = test['itvInterventieOptieId']
x_test2 = test2[features]
y_test2 = test2['itvInterventieOptieId']


# In[11]:


dt = c.fit(X_train, y_train)


# In[12]:


def show_tree(tree, features, path):
    f = io.StringIO()
    export_graphviz(tree, out_file= f, feature_names = features)
    pydotplus.graph_from_dot_data(f.getvalue()).write_png(path)
    img = imageio.imread(path)
    plt.rcParams["figure.figsize"] = (20,20)
    plt.imshow(img)


# In[13]:


show_tree(dt, features, 'test.png')


# In[14]:


y_pred = c.predict(x_test)
y_pred2 = c.predict(x_test2)


# In[15]:


print(y_pred2)


# In[16]:


from sklearn.metrics import accuracy_score
score = accuracy_score(y_test, y_pred) * 100


# In[17]:


print("Accuracy using Decision Tree: ", round(score, 1), "%")


# In[18]:


X_train

