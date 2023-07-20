# webscrapper
Kream.co.kr Product Web Scraper and Data Exporter to Excel

## Description

This web scraper is specifically designed to extract product data from Kream.co.kr, a renowned online retail website. The scraper employs asynchronous programming techniques to efficiently crawl through product pages, download HTML content, and parse crucial information such as the English name, name, brand name, URL, and model number.

## How it Works

The web scraper utilizes a loop to continuously fetch product data until it encounters a specified number of consecutive bad requests. Upon a successful scrape, the parsed information is both displayed on the console and saved in a list of `Product` objects. The scraped data is then exported to an Excel file using the `SaveDataToExcel` function, allowing easy access for further analysis.

## Installation

1. Clone the repository to your local machine.
2. Ensure you have the latest version of .NET Core installed.
3. Run the application using your preferred IDE or the command line.

## Requirements

- .NET Core SDK

## Usage

1. Simply run the application. The scraper will automatically fetch data from the Kream.co.kr website.
2. Monitor the console output to view the progress and the product information being scraped.
3. Once the scraper completes its execution, you will find the collected data saved in the `products.xlsx` Excel file.

## License

This project is licensed under the MIT license .

## Contribution

Contributions are welcome! If you encounter any issues or have ideas for new features, feel free to open a pull request.

## Disclaimer

This web scraper is intended for educational and personal use only. Please be respectful of the website's terms of service and robots.txt file while using this application. The developers are not responsible for any misuse or violations of website policies.

Happy Scraping! 😄
