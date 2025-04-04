import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 50,
  duration: '10s',
};

export default function () {
  const url = 'http://localhost:5000/api/GameOfLife/next';
  const payload = JSON.stringify({
    rows: 3,
    columns: 3,
    cells: [
      [0, 1, 0],
      [0, 1, 0],
      [0, 1, 0]
    ]
  });

  const headers = { 'Content-Type': 'application/json' };
  const res = http.post(url, payload, { headers });

  check(res, {
    'is status 200': (r) => r.status === 200,
  });

  sleep(1);
}