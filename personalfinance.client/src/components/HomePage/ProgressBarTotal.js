import ProgressBar from 'react-bootstrap/ProgressBar';

import React from 'react';

export default function ProgressBarTotal({ totalExpenditures, totalIncome }) {
  const fraction = ((totalExpenditures / totalIncome) * 100).toFixed(2);
  return (
    <>
      {totalExpenditures > 0 && totalIncome > 0 && (
        <>
          <h4 style={{ textAlign: 'center' }}>
            Financial balance : {fraction}%
          </h4>
          <ProgressBar
            style={{ minWidth: '200px', margin: '10px' }}
            now={fraction}
            variant={
              fraction < 75 ? 'success' : fraction < 90 ? 'warning' : 'danger'
            }
          />
        </>
      )}
    </>
  );
}
