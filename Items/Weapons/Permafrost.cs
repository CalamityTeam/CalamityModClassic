using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Permafrost : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/5.Cryogen/Permafrost");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Permafrost");
		Item.width = 64;  //The width of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 30;
		Item.useTime = 30;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 7.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 64;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("Spawns ice bombs that explode after 2 seconds into ice shards on enemy hits");
		Item.value = 200000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		int i = Main.myPlayer;
		float num72 = 6f;
		int num73 = Main.rand.Next(40, 50);
		float num74 = 5f;
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num78 = (float)Main.mouseX + Main.screenPosition.X + vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y + vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + vector2.Y;
		}
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		float num81 = num80;
		if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		{
			num78 = (float)player.direction;
			num79 = 0f;
			num80 = num72;
		}
		else
		{
			num80 = num72 / num80;
		}
    	num78 *= num80;
		num79 *= num80;
		int num107 = 2;
		for (int num108 = 0; num108 < num107; num108++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= (float)(100 * num108);
			num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (num79 < 0f)
			{
				num79 *= -1f;
			}
			if (num79 < 20f)
			{
				num79 = 20f;
			}
			num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			num80 = num72 / num80;
			num78 *= num80;
			num79 *= num80;
			Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, 0f, 0f, Mod.Find<ModProjectile>("IceBombFriendly").Type, num73, num74, i, 0f, (float)Main.rand.Next(3));
		}
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CryoBar", 12);
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
    	if (Main.rand.Next(3) == 0)
    	{
			int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 67, (float)(player.direction * 2), 0f, 150, default(Color), 1.5f);
			Main.dust[num250].velocity *= 0.2f;
    	}
    }
}}
