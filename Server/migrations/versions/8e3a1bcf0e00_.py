"""empty message

Revision ID: 8e3a1bcf0e00
Revises: 
Create Date: 2024-08-13 23:58:52.949457

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '8e3a1bcf0e00'
down_revision = None
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.create_table('clips',
    sa.Column('id', sa.Integer(), nullable=False),
    sa.Column('upload_date', sa.DateTime(), nullable=False),
    sa.Column('token', sa.String(), nullable=False),
    sa.Column('data_object', sa.String(), nullable=False),
    sa.PrimaryKeyConstraint('id')
    )
    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.drop_table('clips')
    # ### end Alembic commands ###
