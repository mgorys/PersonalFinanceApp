const url = 'https://localhost:7226/api/';

export async function getHttp(endpoint, query, token) {
  let wholeUrl = url + endpoint;

  wholeUrl += '?';
  for (const [key, value] of Object.entries(query)) {
    if (value !== null && value !== undefined) wholeUrl += `${key}=${value}&&`;
  }
  let response = await fetch(wholeUrl, {
    headers: {
      Authorization: `Bearer ${token}`,
      'content-type': 'application/json',
    },
    method: 'GET',
    credentials: 'same-origin',
  });
  if (!response.ok) {
    console.log('error with path', wholeUrl);
    return response;
  }
  let result = await response.json();
  const { dataFromServer } = result;
  console.log('data with path', wholeUrl, dataFromServer);
  return dataFromServer;
}
export async function postHttp(endpoint, body, token) {
  let wholeUrl = url + endpoint;
  let response = await fetch(wholeUrl, {
    headers: {
      Authorization: `Bearer ${token}`,
      'content-type': 'application/json',
    },
    body: JSON.stringify(body),
    method: 'POST',
    credentials: 'same-origin',
  });
  if (!response.ok) {
    console.log('error with path', wholeUrl);
    return response;
  }
  let result = await response.json();
  console.log('data with path', wholeUrl, result);
  return result;
}
export async function deleteHttp(endpoint, token) {
  let wholeUrl = url + endpoint;
  let response = await fetch(wholeUrl, {
    headers: {
      Authorization: `Bearer ${token}`,
      'content-type': 'application/json',
    },
    method: 'DELETE',
    credentials: 'same-origin',
  });
  if (!response.ok) {
    console.log('error with path', wholeUrl);
    return response;
  }
  let result = await response.json();
  console.log('data with path', wholeUrl, result);
  return result;
}
