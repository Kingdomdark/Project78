
# coding: utf-8

# In[1]:


import pandas as pd #Dataframe, Series
import numpy as np #Scientific Computing

from sklearn import tree
from sklearn.tree import DecisionTreeClassifier,export_graphviz
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


data = pd.read_csv('C:/Users/JasonTjauw/Desktop/Project78/WindowsFormsApp1/new.csv')



train, test = train_test_split(data, test_size = 0.15)



# In[10]:

c = DecisionTreeClassifier(criterion="entropy", min_samples_split=85)


# In[11]:


features = ['itvid','itvRegieParentId','itvTargetId','probId',
            'lgscoreId','lgscoreScore', 'lgId', 'casId', 'tgid',
           'casTargetId', 'ptid', 'sclSubjectId', 'sclId',
           'sclCollectionId','probProbleemOptieId' ]

# In[12]:


X_train = train[features]
y_train = train["itvInterventieOptieId"]

X_test = test[features]
y_test = test["itvInterventieOptieId"]


# In[13]:


dt = c.fit(X_train, y_train)


# In[14]:


def show_tree(tree, features, path):
    f = io.StringIO()
    export_graphviz(tree, out_file= f, feature_names = features)
    pydotplus.graph_from_dot_data(f.getvalue()).write_png(path)
    img = imageio.imread(path)
    plt.rcParams["figure.figsize"] = (20,20)
    plt.imshow(img)
show_tree(dt, features, 'test_tree.png')


# In[15]:


y_pred = c.predict(X_test)


# In[16]:


y_pred


from sklearn.metrics import accuracy_score
score = accuracy_score(y_test, y_pred) * 100
print("Accuracy using Decision Tree: ", round(score, 1), "%")
