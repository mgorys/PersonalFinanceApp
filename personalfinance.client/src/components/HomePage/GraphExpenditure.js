import React, { useEffect, useState } from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Doughnut } from 'react-chartjs-2';
import { Button, Container, Row, Col } from 'react-bootstrap';

export default function GraphExpenditure({
  fetchedData,
  hasItems,
  setTotalExpenditures,
}) {
  const graphExpenditures = ChartJS.register(ArcElement, Tooltip, Legend);
  const [chartReady, setChartReady] = useState(false);
  const [dataChart, setDataChart] = useState({});
  const [total, setTotal] = useState(0);
  useEffect(() => {
    if (hasItems) LoadChartData();
  }, [fetchedData]);

  function LoadChartData() {
    try {
      const chartEntities = fetchedData.filter(
        (category) => category.type === 0
      );
      const labels = chartEntities.map((category) => category.name);
      const data = chartEntities.map((category) => category.amount);
      const backgroundColor = chartEntities.map((category) => category.color);
      const dataChartReady = {
        labels: labels,
        datasets: [
          {
            label: 'Value',
            data: data,
            backgroundColor: backgroundColor,
            hoverOffset: 20,
          },
        ],
      };
      setDataChart(dataChartReady);
      let total = data.reduce((partialSum, a) => partialSum + a, 0);
      setTotal(total);
      setTotalExpenditures(total);
      setChartReady(true);
    } catch (error) {
      console.log('Chart Data', error);
    }
    return true;
  }
  return (
    <div>
      {chartReady && total > 0 && (
        <>
          <h3>Expenditures</h3>
          <h3>Total: {total}PLN</h3>
          <>
            <Doughnut
              style={{
                minHeight: '200px',
                marginLeft: 'auto',
                marginRight: 'auto',
              }}
              data={dataChart}
            />
          </>
        </>
      )}
    </div>
  );
}
