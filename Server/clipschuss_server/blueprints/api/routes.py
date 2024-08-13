from flask import request, jsonify
from clipschuss_server.blueprints.api import bp
from clipschuss_server.extensions import db
from clipschuss_server.models.clipboard import Clip
from clipschuss_server.utilities import generate_token
import logging


@bp.route('/get_clip/<token>')
def get_clip(token):
    clip: Clip = Clip.query.filter_by(token=token).first()
    return clip.data_object if clip else 'Token not found'


@bp.route('/add_clip', methods=['POST'])
def add_clip():
    try:
        data = request.get_json()

        new_clip: Clip = Clip(
            data_object = data['data_object'],
            token = data['token']
        )

        db.session.add(new_clip)
        db.session.commit()

        return jsonify({'message': 'Ok'}), 201

    except Exception as e:
        logging.exception('Error adding clip.')
        return jsonify({'message': ''}), 500