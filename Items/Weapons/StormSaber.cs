using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class StormSaber : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/StormSaber");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Storm Saber");
		Item.width = 52;  //The width of the .png file in pixels divided by 2.
		Item.damage = 50;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 25;
		Item.useTime = 25;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 6f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 52;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("StormBeam").Type;
		Item.shootSpeed = 12f;
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
		{
			int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 187, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f);
			Main.dust[num250].velocity *= 0.2f;
		}
    }
}}
