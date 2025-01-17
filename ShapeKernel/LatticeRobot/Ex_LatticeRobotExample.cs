﻿//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, LEAP 71 has waived all copyright and
// related or neighboring rights to this PicoGK example code file.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using Leap71.ShapeKernel;
using PicoGK;
using System.Numerics;

namespace PicoGKExamples
{
    ///////////////////////////////////////////////////////////////////////////
    // Below is a static class that implements a single static function
    // that can be called from Library::Go()

    class LatticeRobotExample
    {
        public static void DiamondToGyroidTask()
        {
            var unitCellName = "LatticeRobot-Diamond_TPMS";
            ImplicitUnitCell unitCell = new (Path.Combine(@"..\..\..\LEAP71_ShapeKernel\ShapeKernel\LatticeRobot\LatticeRobot_Library\", unitCellName), LatticeVariant.Thin);

            var box = unitCell.Bounds;  // centered at origin
            box.vecMin.X *= 5;
            box.vecMax.X *= 5;

            unitCell.AdjustParameters = (ImplicitUnitCell thisCell, Vector3 p) => 
            {
                double diamondToGyroid = Math.Clamp((p.X - box.vecMin.X) / (box.vecMax.X - box.vecMin.X), 0, 1);
                thisCell.SetParameter("gyroid", diamondToGyroid);

                double thickness = 4 - 3 * Math.Clamp((p.Z - box.vecMin.Z) / (box.vecMax.Z - box.vecMin.Z), 0, 1);
                thisCell.SetParameter("thickness", thickness);
            };

            try
            {
                Voxels voxL = new(unitCell, box);

                Library.oViewer().SetGroupMaterial(0, "3291a0", 0f, 1f);
                Library.oViewer().Add(voxL);
            }

            catch (Exception e)
            {
                Library.Log($"Failed to run example: \n{e.Message}"); ;
            }
        }

        public static void SpinodalTask()
        {
            var unitCellName = "LatticeRobot-Spinodal_Noise";
            ImplicitUnitCell unitCell = new (Path.Combine(@"..\..\..\LEAP71_ShapeKernel\ShapeKernel\LatticeRobot\LatticeRobot_Library\", unitCellName), LatticeVariant.Thin);

            var box = unitCell.Bounds;  // centered at origin
            box.vecMin.X *= 5;
            box.vecMax.X *= 5;
            box.vecMin.Y *= 5;
            box.vecMax.Y *= 5;

            unitCell.SetParameter("anisotropy", 0.2);

            unitCell.AdjustParameters = (ImplicitUnitCell thisCell, Vector3 p) => 
            {
                var direction = p.Normalize();
                thisCell.SetParameter("direction_x", direction.X);
                thisCell.SetParameter("direction_y", direction.Y);
                thisCell.SetParameter("direction_z", direction.Z);

                double thickness = 4 - 3 * Math.Clamp((p.Z - box.vecMin.Z) / (box.vecMax.Z - box.vecMin.Z), 0, 1);
                thisCell.SetParameter("thickness", thickness);
            };

            try
            {
                Voxels voxL = new(unitCell, box);

                Library.oViewer().SetGroupMaterial(0, "3291a0", 0f, 1f);
                Library.oViewer().Add(voxL);
            }

            catch (Exception e)
            {
                Library.Log($"Failed to run example: \n{e.Message}"); ;
            }
        }
    }

    
}

