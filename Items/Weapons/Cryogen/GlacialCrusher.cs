using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons.Cryogen {
public class GlacialCrusher : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glacial Crusher");
	}

	public override void SetDefaults()
	{
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 59;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 27;
		Item.useTime = 27;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 58;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 450000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Iceberg").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "CryoBar", 10);
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceRod);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Frostburn, 300);
	}
}}