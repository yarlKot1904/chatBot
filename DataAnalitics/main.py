import csv
import json
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score

def calculate_income_spending(last_6_months_data):
    income = 0
    spending = 0
    for i in range(0, len(last_6_months_data), 2):
        income += float(last_6_months_data[i])
        spending += float(last_6_months_data[i + 1])
    return income, spending

def calculate_credit_rating(income, spending):
    if spending == 0:
        return income
    return income / spending

def csv_to_dataframe(csv_file_path):
    data_list = []
    with open(csv_file_path, mode='r', encoding='utf-8') as csv_file:
        csv_reader = csv.DictReader(csv_file)
        for row in csv_reader:
            last_6_months_data = [
                                     row[f'Month_{i}_Income'] for i in range(1, 7)
                                 ] + [
                                     row[f'Month_{i}_Spending'] for i in range(1, 7)
                                 ]

            income, spending = calculate_income_spending(last_6_months_data)
            credit_rating = calculate_credit_rating(income, spending)

            data = {
                "Name": row["Name"],
                "Surname": row["Surname"],
                "Patronymic": row["Patronymic"],
                "Phone": row["Phone"],
                "Email": row["Email"],
                "Birthday": row["Birthday"],
                "Gender": row["Gender"],
                "Balance": int(row["Balance"]),
                "Products": row["Products"],
                "Income": int(income),
                "Spending": int(spending),
                "CreditRating": credit_rating
            }
            data_list.append(data)

    df = pd.DataFrame(data_list)
    return df

def visualize_data(df):
    plt.figure(figsize=(14, 7))
    sns.barplot(data=df, x='Name', y='Income', color='blue', label='Income')
    sns.barplot(data=df, x='Name', y='Spending', color='red', label='Spending')
    plt.xlabel('Users')
    plt.ylabel('Amount')
    plt.title('Income and Spending of Users')
    plt.legend()
    plt.xticks(rotation=45)
    plt.tight_layout()
    plt.show()

    plt.figure(figsize=(10, 6))
    sns.scatterplot(data=df, x='Income', y='CreditRating', hue='Gender')
    plt.xlabel('Income')
    plt.ylabel('Credit Rating')
    plt.title('Credit Rating vs Income')
    plt.tight_layout()
    plt.show()

def machine_learning_analysis(df):
    X = df[['Income', 'Spending']]
    y = df['CreditRating']

    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

    model = LinearRegression()
    model.fit(X_train, y_train)

    y_pred = model.predict(X_test)

    print(f"Mean squared error: {mean_squared_error(y_test, y_pred):.2f}")
    print(f"R^2 score: {r2_score(y_test, y_pred):.2f}")

    plt.figure(figsize=(10, 6))
    sns.regplot(x=y_test, y=y_pred, ci=None, color='blue', marker='o')
    plt.xlabel('Actual Credit Rating')
    plt.ylabel('Predicted Credit Rating')
    plt.title('Actual vs Predicted Credit Rating')
    plt.tight_layout()
    plt.show()

def dataframe_to_json(df, json_file_path):
    data_list = df.to_dict(orient='records')
    with open(json_file_path, mode='w', encoding='utf-8') as json_file:
        json.dump(data_list, json_file, ensure_ascii=False, indent=4)

# Пример использования:
csv_file_path = 'temp.csv'
json_file_path = 'output.json'

df = csv_to_dataframe(csv_file_path)
visualize_data(df)
machine_learning_analysis(df)
dataframe_to_json(df, json_file_path)
