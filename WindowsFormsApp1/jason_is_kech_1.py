
# coding: utf-8

# In[1]:




#import pandas as pd #Dataframe, Series
#import numpy as np #Scientific Computing

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
get_ipython().run_line_magic('matplotlib', 'inline')


# In[2]:


data = pd.read_csv('C:/Users/ricky/OneDrive/Documents/anahmemey/new.csv')


# In[3]:


data.describe()


# In[4]:


data.head()


# In[5]:


train, test = train_test_split(data, test_size = 0.15)


# In[6]:


print("Training size{}: Test size: {}".format(len(train), len(test)))


# In[7]:


train.shape


# In[8]:


neg_test = data[data['lgscoreScore'] == 0]['lgscoreScore']
neutral_test = data[data['lgscoreScore'] == 5]['lgscoreScore']
pos_test = data[data['lgscoreScore'] == 10]['lgscoreScore']

fig = plt.figure(figsize =(12, 8))
plt.title("lgscoreScore")
pos_test.hist(alpha = 1, bins = 1, label = "positive")
neg_test.hist(alpha = 1, bins = 1, label = "negative")
neutral_test.hist(alpha = 1, bins = 1, label = "neutral")
plt.legend(loc= "upper right")


# In[9]:


neutral_test


# In[10]:


c = DecisionTreeClassifier(min_samples_split=85)


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


# In[17]:


from sklearn.metrics import accuracy_score
score = accuracy_score(y_test, y_pred) * 100


# In[18]:


print("Accuracy using Decision Tree: ", round(score, 1), "%")


# In[19]:


X_test

