from flask import Flask, render_template
from config import Config
from clipschuss_server.extensions import db, migrate

def create_app(config_class=Config):
    app = Flask(__name__)
    app.config.from_object(config_class)

    # Initialize Flask extensions here
    db.init_app(app)
    migrate.init_app(app, db)

    # Register blueprints here
    from clipschuss_server.blueprints.api import bp as api_bp
    app.register_blueprint(api_bp, url_prefix='/api')

    # Serve SPA
    @app.route('/', defaults={'path': ''})
    @app.route('/<path:path>')
    def index(path):
        if path == '':
            return render_template('index.html')
        if path == 'spa/index.html':
            return render_template(path)
        elif path == 'spa/about.html':
            return render_template(path)
        elif path.startswith('spa/'):
            return render_template("spa/404.html")
        else:
            return render_template("index.html")

    return app