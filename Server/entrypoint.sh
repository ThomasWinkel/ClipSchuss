#!/usr/bin/env bash
flask db upgrade
flask --app clipschuss_server run --host=0.0.0.0