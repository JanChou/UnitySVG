public class SVGPathSegCurvetoQuadraticSmoothAbs : SVGPathSegCurvetoQuadratic, ISVGDrawableSeg  {
  private float _x = 0f;
  private float _y = 0f;
  //================================================================================
  public float x {
    get{ return this._x;}
  }
  //-----
  public float y {
    get{ return this._y;}
  }
  //================================================================================
  public SVGPathSegCurvetoQuadraticSmoothAbs(float x, float y)
                  : base(SVGPathSegTypes.CurveTo_Quadratic_Smooth_Abs) {
    this._x = x;
    this._y = y;
  }
  //================================================================================
  public override SVGPoint currentPoint{
    get{
      return new SVGPoint(this._x, this._y);
    }
  }
  //-----
  public override SVGPoint controlPoint1{
    get{
      SVGPoint _return = new SVGPoint(0f,0f);
      SVGPathSeg _prevSeg = previousSeg;
      if(_prevSeg != null) {
        SVGPoint t_currP = previousPoint;
        SVGPoint t_prevCP2 = ((SVGPathSegCurvetoQuadratic)_prevSeg).controlPoint1;
        SVGPoint t_P = t_currP - t_prevCP2;
        _return = t_currP + t_P;
      }
      return _return;
    }
  }
  //--------------------------------------------------------------------------------
  //Method: Render
  //--------------------------------------------------------------------------------
  public void Render(SVGGraphicsPath _graphicsPath) {
    SVGPoint p, p1;
    p = currentPoint;
    p1 = controlPoint1;
    _graphicsPath.AddQuadraticCurveTo(p1, p);
  }
}