using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AncientCrusher : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AncientCrusher");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ancient Crusher");
		Item.width = 62;  //The width of the .png file in pixels divided by 2.
		Item.damage = 55;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.useTime = 30;
		Item.useTurn = true;
		////Tooltip.SetDefault("Summons fossil spikes on enemy hits");
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 62;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 150000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Amber, 8);
		recipe.AddIngredient(ItemID.FossilOre, 35);
		recipe.AddIngredient(null, "EssenceofCinder", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FossilSpike").Type, hit.Damage, hit.Knockback, Main.myPlayer);
	}
}}
