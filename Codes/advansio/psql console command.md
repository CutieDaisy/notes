docker run -it --rm  postgres psql -h 172.17.0.1 -U papss
\c papss
\dt

select column_name from information_schema.columns where table_name = 'mwtm_papss_trans'