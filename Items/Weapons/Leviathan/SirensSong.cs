using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan {
public class SirensSong : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Siren's Song");
		//Tooltip.SetDefault("Casts slow-moving treble clefs that confuse enemies");
	}

    public override void SetDefaults()
    {
        Item.damage = 55;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 56;
        Item.height = 50;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6.5f;
        Item.value = 750000;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item26;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SirensSong").Type;
        Item.shootSpeed = 13f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "IOU");
        recipe.AddIngredient(null, "LivingShard");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	    int projectile1 = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    if (Main.rand.NextBool(3))
	    {
	    	Main.projectile[projectile1].penetrate = -1;
	    }
	    return false;
	}
}}