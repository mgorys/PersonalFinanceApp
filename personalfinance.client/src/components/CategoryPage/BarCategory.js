import React, { useState, useEffect } from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

export default function BarCategory({ category, transactionList, hasItems }) {
  const [options, setOptions] = useState({});
  const [dataChart, setDataChart] = useState({});
  const [chartReady, setChartReady] = useState(false);
  ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
  );
  useEffect(() => {
    if (hasItems) LoadChartData();
  }, [transactionList]);

  function LoadChartData() {
    try {
      const optionsData = {
        indexAxis: 'y',
        elements: {
          bar: {
            borderWidth: 2,
          },
        },
        responsive: true,
        plugins: {
          legend: {
            display: false,
            // position: 'right',
          },
          title: {
            display: true,
            text: `${category.name}`,
            font: {
              size: 30,
            },
          },
        },
      };
      const labels = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December',
      ];

      var array = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

      function getArrayValues() {
        transactionList.forEach((element) => {
          const d = new Date(element.putTime);
          const index = d.getMonth();
          array[index] += element.amount;
        });
      }
      getArrayValues();
      const dataChartReady = {
        labels,
        datasets: [
          {
            label: 'Amount',
            data: array,
            backgroundColor: `${category.color}`,
          },
        ],
      };
      setOptions(optionsData);
      setDataChart(dataChartReady);
      setChartReady(true);
    } catch (error) {
      console.log('Chart Data', error);
    }
    return true;
  }

  return (
    <>
      {chartReady && (
        <Bar
          style={{ margin: '50px', maxHeight: '500px' }}
          options={options}
          data={dataChart}
        />
      )}
    </>
  );
}
