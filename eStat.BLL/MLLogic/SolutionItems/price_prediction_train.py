import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.ensemble import RandomForestRegressor
import pickle

numeric_columns = ['BaseProductPrice',
                   'NbOfSearchesLastMonth', 'NbOfPurchasesLastMonth',
                   'CurrentAveragePrice', 'AveragePriceLastMonth',
                   'CurrentAverageStockPerRetailer', 'AverageStockPerRetailerLastMonth',
                   'MyStock', 'MyCurrentPrice',
                   'MyAveragePriceLastMonth', 'MySellsLastMonth']
text_columns = ['ProductName', 'ProductCharacteristics']

filename = 'D:\\Work\\Uni\\Licenta\\eStat\\backend\\eStat.BLL\\MLLogic\\SolutionItems\\rf_model.pkl'
data = pd.read_csv("D:\\Work\\Uni\\Licenta\\eStat\\backend\\eStat.BLL\\MLLogic\\SolutionItems\\train.csv")
y = data['PredictedPrice']
transformed_features = {}

data = data.drop(["UserProductID", "ProductID"], axis=1)

tfidf = TfidfVectorizer(stop_words='english')

for col in text_columns:
    X_transformed = tfidf.fit_transform(data[col])
    transformed_features[col] = pd.DataFrame(X_transformed.toarray(), columns=tfidf.get_feature_names_out())

X = pd.concat([transformed_features[col] for col in text_columns] + [data[numeric_columns]], axis=1)

# X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

rf_classifier = RandomForestRegressor(n_estimators=50, max_depth=10, random_state=42)
rf_classifier.fit(X, y)

y_pred = rf_classifier.predict(X)

with open(filename, 'wb') as file:
    pickle.dump((rf_classifier, tfidf), file)