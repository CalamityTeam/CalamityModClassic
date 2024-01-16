using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class DepthBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/DepthBlade");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Depth Blade");
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 22;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 22;
		Item.useTime = 22;  //Ranges from 1 to 55. 
		////Tooltip.SetDefault("Hitting enemies will cause the crush depth debuff\nThe lower the enemies' defense the more damage they take from this debuff");
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 5.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 48;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 90000;  //Value is calculated in copper coins.
		Item.rare = 2;  //Ranges from 1 to 11.
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 33);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 180);
	}
}}
