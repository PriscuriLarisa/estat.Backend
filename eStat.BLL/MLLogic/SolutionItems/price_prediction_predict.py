import pandas as pd
import csv
import pickle

numeric_columns = ['BaseProductPrice',
                   'NbOfSearchesLastMonth', 'NbOfPurchasesLastMonth',
                   'CurrentAveragePrice', 'AveragePriceLastMonth',
                   'CurrentAverageStockPerRetailer', 'AverageStockPerRetailerLastMonth',
                   'MyStock', 'MyCurrentPrice',
                   'MyAveragePriceLastMonth', 'MySellsLastMonth']
text_columns = ['ProductName', 'ProductCharacteristics']

filename = 'C:\\Users\\prisc\\Downloads\\rf_model.pkl'
data = pd.read_csv("D:\\Work\\Uni\\Licenta\\eStat\\backend\\eStat.BLL\\MLLogic\\SolutionItems\\train.csv")
y = data['PredictedPrice']

with open(filename, 'rb') as file:
    rf_classifier, tfidf = pickle.load(file)

data_test = pd.read_csv("D:\\Work\\Uni\\Licenta\\eStat\\backend\\eStat.BLL\\MLLogic\\SolutionItems\\test.csv")

transformed_features_test = {}
for col in text_columns:
    X_transformed = tfidf.fit_transform(data_test[col])
    transformed_features_test[col] = pd.DataFrame(X_transformed.toarray(), columns=tfidf.get_feature_names_out())
    missingColumns = [i for i in rf_classifier.feature_names_in_ if i not in tfidf.get_feature_names_out()]


X_test = pd.concat([transformed_features_test[col] for col in text_columns] + [data_test[numeric_columns]], axis=1)
X_test = X_test.loc[:, ~X_test.columns.duplicated()]
X_test = X_test.reindex(columns=rf_classifier.feature_names_in_)
X_test = X_test.fillna(0)
prediction = rf_classifier.predict(X_test)
data_test['PredictedPrice'] = prediction
data_test.to_csv("D:\\Work\\Uni\\Licenta\\eStat\\backend\\eStat.BLL\\MLLogic\\SolutionItems\\test.csv", index=False, quoting=csv.QUOTE_NONNUMERIC)
print(prediction)